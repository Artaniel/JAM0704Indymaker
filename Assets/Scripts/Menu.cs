#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;

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

}
