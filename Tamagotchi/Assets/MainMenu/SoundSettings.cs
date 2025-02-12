using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer audioMixer;  
    public string exposedParameter = "MasterVolume";  
    public Slider volumeSlider; 

    void Start()
    {
       
        float currentVolume = 0f;
        bool success = audioMixer.GetFloat(exposedParameter, out currentVolume);
        if (success)
        {
            Debug.Log("Current volume: " + currentVolume); // Log to check value
        }
        else
        {
            Debug.LogWarning("Could not get volume parameter value.");
        }

        float normalizedVolume = Mathf.InverseLerp(-80f, 0f, currentVolume);
        volumeSlider.value = normalizedVolume;

    
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
     
        float volumeInDecibels = Mathf.Lerp(-80f, 0f, sliderValue);

     
        audioMixer.SetFloat(exposedParameter, volumeInDecibels);
    }
}
