using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
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

        // Respawn an enemy immediately after one is destroyed
        SpawnEnemy();
    }
}
