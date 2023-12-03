using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
    public GameObject gameOverPanel;
    public Button restartButton;
    public GameObject prefab;
    public GameObject obstaclesParent;
    public TextMeshProUGUI scoreText;

    GameObject prefabClone;

    HandsGesture handsGesture;

    private void Awake() {
        handsGesture = (HandsGesture)FindObjectOfType(typeof(HandsGesture));       
    }

    private void Start() {
        SpawnPrefab();

        restartButton.onClick.AddListener(() => Restart());
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
