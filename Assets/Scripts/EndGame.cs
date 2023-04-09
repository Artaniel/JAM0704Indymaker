using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void ExitToMenu() {
        GameObject.FindWithTag("GameController").GetComponent<AudioAmb>().StopSoundEvent();
        SceneManager.LoadScene("Menu");
        AudioRoomTone.StopSoundEvent();
    }
}
