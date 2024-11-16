using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public BulletColor color;
    
    public GameObject player;       //Mettre le GameObject du joueur ici
    public float range;             //Max deplacement distance
    public Transform centrePoint;   //Center of area where the enemy move in (or agent transform if don't care)
    public DeplacementBehavior enemyDeplacement;
    private Animator animator;
    public enum DeplacementBehavior {RandomBehavior, ChasingBehavior, ZigZagBehavior}
    
    private NavMeshAgent agent;
    private List<Vector3> waypoints;
    private int currentWaypointIndex = 0;
    private bool _canMove = true;
    private bool coroutineDebug = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        waypoints = new List<Vector3>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!_canMove)
            return;

        if (enemyDeplacement == DeplacementBehavior.RandomBehavior)
        {
            randomDeplacement();  //Deplacement aleatoire sur NavMesh
        }
        else if (enemyDeplacement == DeplacementBehavior.ChasingBehavior)
        {
            goTowardPlayer();     //Deplacement chase joueur
        }
        else if (enemyDeplacement == DeplacementBehavior.ZigZagBehavior)
        {
            zigZagDeplacement();  //Deplacement zigZag vers joueur
        }
        animator.SetFloat("velocity", agent.velocity.magnitude);
    }

    void goTowardPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    void zigZagDeplacement()
    {
        if(waypoints.Count == 0)
        {
            zigZagProcess();
            return;
        }

        //Si on est assez proche de notre pts d'arriver
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            //Incremente notre index
            currentWaypointIndex = currentWaypointIndex + 1;
            //Si egale a zero alors clear le tableau pour generer un novueau zigZag
            if (currentWaypointIndex == waypoints.Count)
            {
                waypoints.Clear();
                currentWaypointIndex = 0;
                zigZagProcess();
                return;
            }
        }

        agent.SetDestination(waypoints[currentWaypointIndex]);
    }

    void zigZagProcess()
    {
        //On decompose notre chemin en 4 sous points
        float divX = (player.transform.position.x - transform.position.x) / 4;
        float divZ = (player.transform.position.z - transform.position.z) / 4;

        //Decoupage du chemin
        for (int i = 1; i < 4; i++)
        {
            float xVal = transform.position.x + divX * i;
            float zVal = transform.position.z + divZ * i;

            Vector3 waypointToAdd = new Vector3(xVal, transform.position.y, zVal);

            if (i == 1 || i == 3)
            {
                waypointToAdd.x += 5;
            }
            else if (i == 2)
            {
                if (transform.position.x < 0 && transform.position.z > 0)
                {
                    waypointToAdd.z -= 5;
                }
                else
                {
                    waypointToAdd.z += 5;
                }
            }

            waypoints.Add(waypointToAdd);
        }

        //Ajout point final
        waypoints.Add(player.transform.position);
    }

    void randomDeplacement()
    {
        //Si on a pas parcouru toute notre distance
        if (agent.remainingDistance <= agent.stoppingDistance && coroutineDebug)
        {
            StartCoroutine(randomDeplacementCoroutine(5));
        }
    }

    //Tire un point aleatoire en tenant compte du centre defini et de la range
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        //Selectionne un point aleatoire en fonction du centre defini + de la range
        Vector3 randomPoint = center + Random.insideUnitSphere * range;

        //The 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
        //or add a for loop like in the documentation
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }


    IEnumerator randomDeplacementCoroutine(int time)
    {
        //On calcul un nouveau point
        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point))
        {
            coroutineDebug = false;
            yield return new WaitForSeconds(time);
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            agent.SetDestination(point);
        }
        coroutineDebug = true;
        yield break;
    }


    public void Paralyze(BulletColor bulletColor, float duration)
    {
        if (bulletColor == color)
            StartCoroutine(StopMovementTimer(duration));
        // else
        // die??? + gameOver
    }

    /// <summary>
    /// Stops player movement for a parametrized amount of time
    /// </summary>
    /// <param name="timer">Amount of time the AI should stop moving</param>
    /// <returns></returns>
    private IEnumerator StopMovementTimer(float timer)
    {
        _canMove = false;
        yield return new WaitForSeconds(timer);
        _canMove = true;
    }
}