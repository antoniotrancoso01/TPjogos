using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sensitivitySlider;
    public Slider volumeSlider;

    private void Start()
    {
        //Sens por defeito
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 5f);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0f);
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    //Função do slider
    public void SetMouseSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }

}
