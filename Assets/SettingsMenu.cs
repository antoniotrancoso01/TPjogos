using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    //public Slider sensitivitySlider;

    //public float defaultSens = 1.0f;
    //public static float mouseSensitivity = 1.0f;

    //private void Start()
    //{
    //    sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", defaultSens);
    //    mouseSensitivity = sensitivitySlider.value;

    //    sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    //}

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }


    //public void UpdateSensitivity(float newSensitivity)
    //{
    //    mouseSensitivity = newSensitivity;
    //    PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
    //}
}
