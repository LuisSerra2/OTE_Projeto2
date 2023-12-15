using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCammController : Singleton<WebCammController> {

    public GameObject blobPrefab;
    public float threshold = 0.1f;
    private bool isTracking = false;
    private GameObject blobClone;

    private WebCamTexture webcamTexture;
    private Color selectedColor;

    WebCamDevice[] devices;
    int deviceIndex = 0;

    public Action ON_FIST_EVENT;
    public Action ON_RPALM_EVENT;

    public bool canClick = false;

    void Start() {
        devices = WebCamTexture.devices;
        if (devices.Length == 0) {
            Debug.LogError("Nenhuma webcam encontrada.");
            return;
        }

        webcamTexture = new WebCamTexture(devices[deviceIndex].name);

        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            deviceIndex++;
            webcamTexture = new WebCamTexture(devices[deviceIndex].name, 640, 320);

            GetComponent<Renderer>().material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }

        if (Input.GetMouseButtonDown(0) && canClick) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                Vector2 clickPos = hit.textureCoord;
                selectedColor = GetColorFromWebcamTexture(clickPos);
                isTracking = true;
                canClick = false;
            }
        }

        if (isTracking) {
            TrackColor(selectedColor);
        }
    }

    Color GetColorFromWebcamTexture(Vector2 uv) {
        Color[] pixels = webcamTexture.GetPixels();
        int x = (int)(uv.x * webcamTexture.width);
        int y = (int)(uv.y * webcamTexture.height);
        int index = y * webcamTexture.width + x;
        return pixels[index];
    }

    void TrackColor(Color targetColor) {
        Color[] pixels = webcamTexture.GetPixels();
        Vector2 targetPosition = Vector2.zero;
        int count = 0;

        for (int i = 0; i < pixels.Length; i++) {
            if (ColorCloseEnough(pixels[i], targetColor)) {
                float posX = i % webcamTexture.width;
                float posY = i / webcamTexture.width;

                targetPosition += new Vector2(posX, posY);
                count++;
            }
        }

        if (count > 0) {
            targetPosition /= count;

            float normalizedX = targetPosition.x / webcamTexture.width;
            float normalizedY = targetPosition.y / webcamTexture.height;

            Vector3 screenPos = new Vector3(normalizedX * Screen.width, normalizedY * Screen.height, 10f);

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            worldPos.x *= -1;

            float halfScreenHeight = Screen.height / 2f;
            if (screenPos.y > halfScreenHeight) {
                if (Player.Instance.gameObject != null) {
                    ON_RPALM_EVENT?.Invoke();
                }
            } else {
                if (Player.Instance.gameObject != null) {
                    ON_FIST_EVENT?.Invoke();
                }
            }
        }
    }

    bool ColorCloseEnough(Color a, Color b) {
        return Mathf.Abs(a.r - b.r) < threshold &&
               Mathf.Abs(a.g - b.g) < threshold &&
               Mathf.Abs(a.b - b.b) < threshold;
    }
}
