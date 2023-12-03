using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private float score;

    private void Update() {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            score += 1.6f * Time.deltaTime;
        }
    }

    public void ChangeScore(int score) {
        this.score = score;
    }

    public int GetScore() {
        return (int)score;
    }
}
