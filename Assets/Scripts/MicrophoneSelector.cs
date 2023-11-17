using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MicrophoneSelector : MonoBehaviour
{
    public TMP_Dropdown sourceDropdown;
    public int chosenDeviceIndex = 0;

    public static UnityAction<int> OnMicrophoneChoiceChanged;

    private void Start() {
        PopulateSourceDropDown();
    }

    private void PopulateSourceDropDown() {
        var options = new List<TMP_Dropdown.OptionData>();

        foreach (var microphone in Microphone.devices) {
            TMPro.TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(microphone, null);

            options.Add(optionData);
        }

        sourceDropdown.options = options;
    }

    public void ChooseMicrophone(int optionIndex) {
        chosenDeviceIndex = optionIndex;
        OnMicrophoneChoiceChanged?.Invoke(chosenDeviceIndex);
    }
}
