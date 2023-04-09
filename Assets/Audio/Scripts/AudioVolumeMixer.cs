using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeMixer : MonoBehaviour
{

    public UnityEngine.UI.Slider sliderMaster;
    public UnityEngine.UI.Slider sliderMusic;
    public UnityEngine.UI.Slider sliderSFX;
    public UnityEngine.UI.Slider sliderUI;

    private FMOD.Studio.VCA vcaMaster;
    private FMOD.Studio.VCA vcaMusic;
    private FMOD.Studio.VCA vcaSFX;
    private FMOD.Studio.VCA vcaUI;

    public float volumeMaster;
    public float volumeMusic;
    public float volumeSFX;
    public float volumeUI;

    private void Start()
    {
        vcaMaster = FMODUnity.RuntimeManager.GetVCA("vca:/VCA_MasterVolume");
        vcaMusic = FMODUnity.RuntimeManager.GetVCA("vca:/VCA_MusicVolume");
        vcaSFX = FMODUnity.RuntimeManager.GetVCA("vca:/VCA_SFXVolume");
        vcaUI = FMODUnity.RuntimeManager.GetVCA("vca:/VCA_UIVolume");

        vcaMaster.getVolume(out volumeMaster);
        sliderMaster.value = volumeMaster;
        vcaMusic.getVolume(out volumeMusic);
        sliderMusic.value = volumeMusic;
        vcaSFX.getVolume(out volumeSFX);
        sliderSFX.value = volumeSFX;
        vcaUI.getVolume(out volumeUI);
        sliderUI.value = volumeUI;

    }

    public void VCAMasterVolumeChange()
    {
        vcaMaster.setVolume(sliderMaster.value);
        volumeMaster = sliderMaster.value;
    }
    public void VCAMusicVolumeChange()
    {
        vcaMusic.setVolume(sliderMusic.value);
        volumeMusic = sliderMusic.value;
    }
    public void VCASFXVolumeChange()
    {
        vcaSFX.setVolume(sliderSFX.value);
        volumeSFX = sliderSFX.value;
    }
    public void VCAUIVolumeChange()
    {
        vcaUI.setVolume(sliderUI.value);
        volumeUI = sliderUI.value;
    }
}
