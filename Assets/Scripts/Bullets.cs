using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {
    public GameObject bulletPrefab;
    public GameObject bulletSpawnPosition;

    [SerializeField] private float shootingForce;

    public static bool canFire = true;

    private void OnEnable() {
        //FillFromMicrophone.OnScreamDetected += SpawnBullets;
        AudioLoudnessDetector.OnScreamDetected += SpawnBullets;
    }
    private void OnDisable() {
        //FillFromMicrophone.OnScreamDetected -= SpawnBullets;
        AudioLoudnessDetector.OnScreamDetected -= SpawnBullets;
    }

    private void SpawnBullets() {

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() {
        canFire = false;
        var bulletClone = Instantiate(bulletPrefab.GetComponent<Rigidbody>(), bulletSpawnPosition.transform.position, Quaternion.identity);

        bulletClone.velocity = transform.forward * shootingForce;


        yield return new WaitForSeconds(.1f);
        canFire = true;

        Destroy(bulletClone.gameObject, 1f);
    }
}
