using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
   /* [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private float maxZ;
    [SerializeField] private float minZ;
    [SerializeField] private int initialEnemyCount;

    private int currentEnemyCount;

    private void Start()
    {
        currentEnemyCount = initialEnemyCount;
        Spawn(initialEnemyCount);
    }

    private void Spawn(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        GameObject enemy = Instantiate(enemyPrefab, transform.position + new Vector3(randomX, -1, randomZ), Quaternion.identity);
        enemy.transform.rotation = Quaternion.Euler(0, 90, 90);
    }

    public void RespawnEnemy()
    {
        currentEnemyCount--;

        SpawnEnemy();
    } */

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoint;

    private void Start()
    {
        for (int i = 0; i < 5; i++) {
            RespawnEnemy(spawnPoint[i].position);
        }
    }

    private void RespawnEnemy(Vector3 spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        enemy.transform.rotation = Quaternion.Euler(0, 90, 90);
    }
  
}
