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
    public GameObject playPanel;
    public GameObject prefab;
    public GameObject prefabWebcam;
    public GameObject obstaclesParent;

    public Button restartButton;
    public Button startButton;
    public Button playButton;

    public TextMeshProUGUI scoreText;

    GameObject prefabClone;


    public bool gameStart = false;

    private void Start() {
        restartButton.onClick.AddListener(() => Restart());
        startButton.onClick.AddListener(() => StartButton());
        playButton.onClick.AddListener(() => PlayButton());
    }


    private void Update() {

        if (GameObject.FindGameObjectWithTag("Player") == null) {
            gameOverPanel.SetActive(true);
            ScoreManager.Instance.ChangeScore(0);
            foreach (Transform item in obstaclesParent.transform) {
                for (int i = 0; i < item.childCount; i++) {
                    Destroy(item.GetChild(i).gameObject);
                }
            }
            gameStart = false;
        } else {
            gameOverPanel.SetActive(false);
            scoreText.text = "Score: " + ScoreManager.Instance.GetScore().ToString();
        }
    }
    private void StartButton() {
        startPanel.SetActive(false);
        playPanel.SetActive(true);
        SpawnWebCamera();
        SpawnPrefab();
        WebCammController.Instance.canClick = true;
    }

    private void PlayButton() {
        playPanel.SetActive(false);
        gameStart = true;
    }

    private void Restart() {
        Destroy(prefabClone);
        SpawnPrefab();
        gameStart = true;
    }

    private void SpawnPrefab() {
        prefabClone = Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    private void SpawnWebCamera() {
        Instantiate(prefabWebcam, new Vector3(0, 1, 0), Quaternion.identity);
    }

    public bool CanPlay() {
        return gameStart;
    }
}
