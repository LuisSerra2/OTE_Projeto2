using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class AudioLoudnessDetector : MonoBehaviour
{
    public int sampleWindow = 64;

    private AudioClip _microphoneClip;
    private string _microphoneName;

    private void Start() {
        MicrophoneToAudioClip(0);
    }

    private void OnEnable() {
        MicrophoneSelector.OnMicrophoneChoiceChanged += ChangeMicrophoneSource;
    }

    private void OnDisable() {
        MicrophoneSelector.OnMicrophoneChoiceChanged -= ChangeMicrophoneSource;
    }

    private void ChangeMicrophoneSource(int deviceIndex) {
        MicrophoneToAudioClip(deviceIndex);
    }

    private void MicrophoneToAudioClip(int microphoneIndex) {

         _microphoneName = Microphone.devices[microphoneIndex];
        _microphoneClip = Microphone.Start(_microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone() {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(_microphoneName), _microphoneClip);
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip) {

        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0) return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudeness = 0;

        foreach (var sample in waveData) {
            totalLoudeness += Mathf.Abs(sample);
        }

        return totalLoudeness / sampleWindow;
    }
}
