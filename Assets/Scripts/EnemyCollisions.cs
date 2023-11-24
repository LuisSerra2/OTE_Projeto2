using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour {

    CountorFinder finder;

    float speed = -5f;
    float lastY = 0f;

    private void Start() {
        finder = (CountorFinder)FindObjectOfType(typeof(CountorFinder));
    }

    private void Update() {
        float step = speed * Time.deltaTime;
        float norm = Mathf.Clamp(finder.testeY - lastY, -1, 1);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + norm, transform.position.z), step);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        gameObject.transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(1f);
        gameObject.transform.localScale = new Vector3(1, .2f, 1);

    }
}
