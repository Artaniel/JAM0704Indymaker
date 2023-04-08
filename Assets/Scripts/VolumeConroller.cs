using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeConroller : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public string volumeParametr = "Master Volume";
    private const float multiplier = 20f;
    private void Awake()
    {
        slider.onValueChanged.AddListener(SliderValueChanged);
    }
    public void SliderValueChanged(float value)
    {
        var volumeValue = Mathf.Log10(value) * multiplier;
        mixer.SetFloat(volumeParametr, volumeValue);
    }
}
