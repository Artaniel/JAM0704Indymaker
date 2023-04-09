using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSceneTransition : MonoBehaviour
{
    public static AudioSceneTransition instance;
    public MusicScript musicScript;
    public AudioVolumeMixer audioVolumeMixer;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject); // to prevent duplicate
    }

    public static void RefreshLinks(Slider masterSlider, Slider musicSlider, Slider SFXSlider, Slider UISlider) {
        if (!instance){ //in case of Refresh started earlier than instance is choisen
            GameObject.Find("MainAudio").GetComponent<AudioSceneTransition>().Awake();        
        }
        instance.audioVolumeMixer.sliderMaster = masterSlider;
        instance.audioVolumeMixer.sliderMusic = musicSlider;
        instance.audioVolumeMixer.sliderSFX = SFXSlider;
        instance.audioVolumeMixer.sliderUI = UISlider;
    }

    public static void UpdateVolumeAll() {
        instance.audioVolumeMixer.VolumeChangeAll();
    }

    public static void ChangeMusic(int index) {
        if (instance)
            instance.musicScript.musicCondition = index;
        else
            Debug.LogWarning("No audio instance");
    }

}
