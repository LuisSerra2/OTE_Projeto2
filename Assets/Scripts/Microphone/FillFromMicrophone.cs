using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FillFromMicrophone : MonoBehaviour
{
    public Image audioBar;
    public Slider sensibilitySlider;
    public AudioLoudnessDetector detector;

    public float minimumSensibility = 100;
    public float maximumSensibility = 1000;
    public float currentLoudnessSensibilty = 500f;
    public float threshold = 0.1f;

    public static UnityAction OnScreamDetected;

    private void Start() {
        if (sensibilitySlider == null) return;

        sensibilitySlider.value = .5f;
        SetLoudnessSensibility(sensibilitySlider.value);
    }

    private void Update() {
        float loudness = detector.GetLoudnessFromMicrophone() * currentLoudnessSensibilty;

        if (loudness < threshold) loudness = 0.01f;

        audioBar.fillAmount = loudness;

        if (loudness > .5f && FireBullets.Instance.canFire && UIManager.Instance.gameStart) OnScreamDetected?.Invoke();

    }

    public void SetLoudnessSensibility(float t) {
        currentLoudnessSensibilty = Mathf.Lerp(minimumSensibility, maximumSensibility, t);
    }
}
