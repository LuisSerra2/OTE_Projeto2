using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour {

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
