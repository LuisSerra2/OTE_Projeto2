using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : Singleton<FireBullets> {
    public Transform bulletsSpawnPoint;
    public GameObject bulletPrefab;

    public float speed;
    float cooldown = 4f;
    float _cooldown;

    public bool canFire = true;

    private void Start() {
        _cooldown = cooldown;
    }

    private void OnEnable() {
        FillFromMicrophone.OnScreamDetected += SpawnAndShoot;
    }

    private void OnDisable() {
        FillFromMicrophone.OnScreamDetected -= SpawnAndShoot;
    }

    private void Update() {
        if (_cooldown < cooldown) {
            canFire = false;
            _cooldown += Time.deltaTime;
            if (_cooldown >= cooldown) {
                canFire = true;
            }
        }
    }

    private void SpawnAndShoot() {
        var bulletClone = Instantiate(bulletPrefab, bulletsSpawnPoint.position, bulletsSpawnPoint.rotation);
        bulletClone.GetComponent<Rigidbody>().velocity = bulletsSpawnPoint.right * speed;
    }


}
