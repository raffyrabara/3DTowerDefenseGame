using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform Spawn1;
    public Transform Spawn2;
    public Transform Waypoint1;
    public Transform Waypoint2;
    public GameObject enemyPrefab;

    void Start()
    {
        SpawnWave();
    }

    void SpawnWave()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnEnemy(Spawn1, Waypoint1);
        }

        for (int i = 0; i < 3; i++)
        {
            SpawnEnemy(Spawn2, Waypoint2);
        }
    }

    void SpawnEnemy(Transform spawnPoint, Transform targetWaypoint)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        // Assuming you have an EnemyMovement script on your enemy objects
        EnemyMovement enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        enemyMovement.SetTargetWaypoint(targetWaypoint);
    }
}
