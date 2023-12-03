using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
    public GameObject gameOverPanel;
    public GameObject startPanel;
    public GameObject prefab;
    public GameObject obstaclesParent;

    public Button restartButton;
    public Button startButton;

    public TextMeshProUGUI scoreText;

    GameObject prefabClone;

    HandsGesture handsGesture;

    public bool gameStart = false;

    private void Awake() {
        handsGesture = (HandsGesture)FindObjectOfType(typeof(HandsGesture));       
    }

    private void Start() {
        restartButton.onClick.AddListener(() => Restart());
        startButton.onClick.AddListener(() => StartButton());
    }


    private void Update() {
        if (GameObject.FindGameObjectWithTag("Player") == null) {
            gameOverPanel.SetActive(true);
            ScoreManager.Instance.ChangeScore(0);
        } else {
            gameOverPanel.SetActive(false);
            scoreText.text = "Score: " + ScoreManager.Instance.GetScore().ToString();
        }
    }
    private void StartButton() {
        startPanel.SetActive(false);
        gameStart = true;
        SpawnPrefab();
    }



    private void Restart() {
        foreach (Transform item in obstaclesParent.transform) {
            for (int i = 0; i < item.childCount; i++) {
                Destroy(item.GetChild(i).gameObject);
            }
        }
        Destroy(prefabClone);
        SpawnPrefab();
    }

    private void SpawnPrefab() {
        prefabClone = Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
}
