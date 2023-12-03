using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[SelectionBase]
public class Player : Singleton<Player> {

    public float speed = 10f;

    int playerMovemetnIndex;

    HandsGesture handsGesture;
    Rigidbody rb;


    private void Awake() {
        handsGesture = (HandsGesture)FindObjectOfType(typeof(HandsGesture));
        rb = GetComponent<Rigidbody>();
    }
    private void Start() {
        handsGesture = (HandsGesture)FindObjectOfType(typeof(HandsGesture));
    }

    private void OnEnable() {
        handsGesture.ON_FIST_EVENT += MoveDownEvent;
        handsGesture.ON_RPALM_EVENT += MoveUpEvent;
    }


    private void OnDisable() {
        handsGesture.ON_FIST_EVENT -= MoveUpEvent;
        handsGesture.ON_RPALM_EVENT -= MoveUpEvent;
    }
    private void MoveUpEvent() {
        playerMovemetnIndex = 0;
    }

    private void MoveDownEvent() {
        playerMovemetnIndex = 1;
    }

    private void Update() {

        PlayerMovementHandler();
    }

    private void FixedUpdate() {
        
        rb.velocity = new Vector3(0, transform.position.y , 0);
    }

    private void PlayerMovementHandler() {
        float step = speed * Time.fixedDeltaTime;

        switch (playerMovemetnIndex) {
            case 0:
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, 4), step);
                break;
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, -2), step);
                break;
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}
