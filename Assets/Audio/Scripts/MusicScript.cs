using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    
    public FMODUnity.EventReference musicEvent;
    FMOD.Studio.EventInstance musicInstance;
    public int musicCondition;
    public bool debug;

    // Start is called before the first frame update
    void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        musicInstance.setParameterByName("SWITCH_Music", 0);
        musicInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        musicInstance.setParameterByName("SWITCH_Music", musicCondition);
        if(musicCondition == 4)
        {
            StartCoroutine(WinningDelay());
        }

        
        if (debug)
        {
            Debug.Log("Music cond " + musicCondition);
        }
    }
    IEnumerator WinningDelay()
    {
        yield return new WaitForSeconds(5f);
        musicCondition = 1;
    }
}
