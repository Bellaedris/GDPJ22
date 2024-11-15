using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float range;             //Max deplacement distance
    public Transform centrePoint;   //Center of area where the enemy move in (or agent transform if don't care)
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //randomDeplacement();
        //goTowardPlayer();
    }

    void goTowardPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("MainCamera");
        agent.destination = player.transform.position;
    }

    void randomDeplacement()
    {
        //Si on a pas parcouru toute notre distance
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            //On calcul un nouveau point
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
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
}