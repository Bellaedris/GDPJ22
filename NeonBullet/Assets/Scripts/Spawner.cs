using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] enemies;
    public AudioManager audioManager;
    public GameObject player;

    public void Start()
    {
        InvokeRepeating("spawn", 30f, 15f);
    }

    void spawn()
    {
        int randomDoor = Random.Range(0, doors.Length);
        int randomEnemies = Random.Range(0, enemies.Length);

        GameObject gm = Instantiate(enemies[randomEnemies]);
        gm.transform.position = doors[randomDoor].transform.position;
        // gm.GetComponent<EnemyAI>().player = player;
        // gm.GetComponent<EnemyAI>()._audioManager = audioManager;
        //gm.GetComponent<EnemyAI>().enemyDeplacement = gm.GetComponent<EnemyAI>().DeplacementBehavior.ChasingBehavior;
    }
}
