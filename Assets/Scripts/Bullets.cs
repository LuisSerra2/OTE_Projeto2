using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {
    public GameObject bulletPrefab;
    public GameObject bulletSpawnPosition;

    public static bool canFire = true;

    private void OnEnable() {
        FillFromMicrophone.OnScreamDetected += SpawnBullets;
    }
    private void OnDisable() {
        FillFromMicrophone.OnScreamDetected -= SpawnBullets;
    }

    private void SpawnBullets() {

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() {
        canFire = false;
        var bulletClone = Instantiate(bulletPrefab.GetComponent<Rigidbody>(), bulletSpawnPosition.transform.position, Quaternion.identity);

        bulletClone.velocity = transform.forward * 10;


        yield return new WaitForSeconds(.1f);
        canFire = true;

        Destroy(bulletClone.gameObject, 1f);
    }
}