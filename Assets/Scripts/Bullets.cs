using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {
    public GameObject bulletPrefab;
    public GameObject bulletSpawnPosition;

    [SerializeField] private float shootingForce;

    public static bool canFire = true;

    float speed = 5f;
    float lastY = 0;

    CountorFinder finder;

    private void Start() {
        finder = (CountorFinder)FindObjectOfType(typeof(CountorFinder));
    }

    private void OnEnable() {
        FillFromMicrophone.OnScreamDetected += SpawnBullets;
    }
    private void OnDisable() {
        FillFromMicrophone.OnScreamDetected -= SpawnBullets;
    }

    private void Update() {
        float step = speed * Time.deltaTime;
        float norm = Mathf.Clamp(finder.testeY - lastY, -1, 1);

        bulletSpawnPosition.transform.position = Vector3.MoveTowards(bulletSpawnPosition.transform.position, new Vector3(bulletSpawnPosition.transform.position.x, bulletSpawnPosition.transform.position.y + norm, bulletSpawnPosition.transform.position.z), step);
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
