using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    private EnemyWaves ew;
    private void Awake()
    {
        ew = FindObjectOfType<EnemyWaves>();
    }
    private void OnTriggerEnter(Collider other)
    {
        ew.RespawnEnemy();
        Destroy(gameObject);
    }
}
