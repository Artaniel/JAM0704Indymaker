using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public float minLoadingTime = 1f;
    private float loadingStartTime;
    public void ExitToMenu() {
        // GameObject.FindWithTag("GameController").GetComponent<AudioAmb>().StopSoundEvent();
        // AudioRoomTone.StopSoundEvent();
        //SceneManager.LoadScene("Menu");
        StartCoroutine(LoadMenuAsync());
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
