#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider SFXVolume;
    public Slider UIVolume;

    private void Awake()
    {
        AudioSceneTransition.RefreshLinks(masterVolume, musicVolume, SFXVolume, UIVolume);
        if (SceneManager.GetActiveScene().name == "Menu")
            AudioSceneTransition.ChangeMusic(0);
        else if (SceneManager.GetActiveScene().name == "Main")
            AudioSceneTransition.ChangeMusic(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (!menu.activeSelf)
            {
                menu.SetActive(true);
                AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 0);
            }
            else
            {
                menu.SetActive(false);
                AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 1);
            }
    }

    public void NewGameButton() {
        SceneManager.LoadScene("Main");
        AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
    }

    public void SettingsButton() {
        menu.SetActive(true);
        AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
    }

    public void ExitButton()
    {
        GameObject.FindWithTag("GameController").GetComponent<AudioAmb>().StopSoundEvent();
        AudioRoomTone.StopSoundEvent();
        AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }

    public void ExitToMenu()
    {
        AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
        SceneManager.LoadScene("Menu");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 1);
    }

    public static void ExitToMenuStatic()
    {
        AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
        SceneManager.LoadScene("Menu");
    }

    public void UpdateVolume()
    {
        AudioSceneTransition.UpdateVolumeAll();
    }
}
