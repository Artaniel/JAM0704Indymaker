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
            }
            else
            {
                menu.SetActive(false);
            }
    }

    public void NewGameButton() {
        SceneManager.LoadScene("Main");
    }

    public void SettingsButton() {
        menu.SetActive(true);
    }

    public void ExitButton() { 
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void ExitToMenuStatic() {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateVolume() {
        AudioSceneTransition.UpdateVolumeAll();
    }
}
