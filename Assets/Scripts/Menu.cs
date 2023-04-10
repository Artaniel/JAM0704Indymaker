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

    public float minLoadingTime = 1f;
    private float loadingStartTime;

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
                //AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 0);
            }
            else
            {
                menu.SetActive(false);
                //AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 1);
            }
    }

    public void NewGameButton() {

        StartCoroutine(LoadGameAsync());

       // SceneManager.LoadScene("Main");
        //AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
    }

    public void SettingsButton() {
        menu.SetActive(true);
        //AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
    }

    public void ExitButton()
    {
        //GameObject.FindWithTag("GameController").GetComponent<AudioAmb>().StopSoundEvent();
        //AudioRoomTone.StopSoundEvent();
        //AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }

    public void ExitToMenu()
    {
        //AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
        StartCoroutine(LoadMenuAsync());
        //SceneManager.LoadScene("Menu");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 1);
    }

    public static void ExitToMenuStatic()
    {
        //AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseClickSound();
        
        SceneManager.LoadScene("Menu");
        
    }

    public void UpdateVolume()
    {
        AudioSceneTransition.UpdateVolumeAll();
    }

    IEnumerator LoadGameAsync()
    {
        loadingStartTime = Time.time;
        AsyncOperation async = SceneManager.LoadSceneAsync("Main");
        async.allowSceneActivation = false;
        FMODUnity.RuntimeManager.LoadBank("/GamplayBank", true);
        while (!FMODUnity.RuntimeManager.HasBankLoaded("/GamplayBank"))
        {
            yield return null;
        }
        while (FMODUnity.RuntimeManager.AnySampleDataLoading())
        {
            yield return null;
        }
        while (Time.time < loadingStartTime + minLoadingTime)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
        while (!async.isDone)
        {
            yield return null;
        }
    }
    IEnumerator LoadMenuAsync()
    {
        loadingStartTime = Time.time;
        AsyncOperation async = SceneManager.LoadSceneAsync("Menu");
        async.allowSceneActivation = false;
        FMODUnity.RuntimeManager.UnloadBank("/GamplayBank");
        while (Time.time < loadingStartTime + minLoadingTime)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
