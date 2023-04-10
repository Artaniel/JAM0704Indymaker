using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public float minLoadingTime = 1f;
    private float loadingStartTime;
    private void Start()
    {
        StartCoroutine(LoadGameAsync());
    }
    IEnumerator LoadGameAsync()
    {
        loadingStartTime = Time.time;
        AsyncOperation async = SceneManager.LoadSceneAsync("Menu");
        async.allowSceneActivation = false;
        FMODUnity.RuntimeManager.LoadBank("/Master", true);
        FMODUnity.RuntimeManager.LoadBank("/Master.strings", true);
        
        while (!FMODUnity.RuntimeManager.HaveAllBanksLoaded)
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
}
