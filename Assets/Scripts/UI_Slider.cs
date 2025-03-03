using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using Unity.Mathematics;

public class UI_Slider : MonoBehaviour
{
    [SerializeField] AudioMixer audioMix;
    float multiplier = 20;
    [SerializeField] Slider slider;

    float volumeScale;
  
   // public float ooo;
     [SerializeField] TMP_Text valueText;
    public string parameter;

    void Start()
    {
        slider.value = Mathf.Log(PlayerPrefs.GetFloat(parameter, 1)* multiplier) ;
        audioMix.SetFloat(parameter,Mathf.Log10(PlayerPrefs.GetFloat(parameter, 1)* 20));
    }

    public void SliderValue(float value)
    {
        volumeScale = value;
        valueText.text = $"{Mathf.RoundToInt(value * 100) + " %"}";

        // logrithmic value.
        audioMix.SetFloat(parameter, Mathf.Log10(value) * multiplier);

        //SaveValues(value);
    }

    public void SaveValues(float value)
    {
        volumeScale = value;
        PlayerPrefs.SetFloat(parameter, value); // using the exposed strings of the mixer
        PlayerPrefs.Save();
    }

    public float VolumeScale()
    {
        return volumeScale;
    }

}
