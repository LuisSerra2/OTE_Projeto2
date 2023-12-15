using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : Singleton<SpawnObstacles> {
    public GameObject[] obstacle;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;
    public float timeBetweenSpawn;
    private float spawnTime;

    [HideInInspector]
    public GameObject obstacleClone;

    private void Update() {
        if(UIManager.Instance.CanPlay()){
            TimerAndSpawn();
        }
    }
    public void TimerAndSpawn() {
        if (Time.time > spawnTime) {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    void Spawn() {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        obstacleClone = Instantiate(obstacle[Random.Range(0, obstacle.Length - 1)], transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity);
        obstacleClone.transform.SetParent(UIManager.Instance.obstaclesParent.transform);
    }
}
