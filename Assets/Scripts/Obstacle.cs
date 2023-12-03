using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject player;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Border") {
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Player") {
            Destroy(player.gameObject);
        }
    }
}
