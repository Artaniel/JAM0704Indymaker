using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNodeScript : MonoBehaviour
{
    public FMODUnity.EventReference soundEvent;
    FMOD.Studio.EventInstance soundInstance;
    public bool nodeConection = false;
    private void Update()
    {
        if (nodeConection)
        {
            StartSound();
        }
        else
        {
            StopSound();
        }
    }

    public void StartSound()
    {
        soundInstance = FMODUnity.RuntimeManager.CreateInstance(soundEvent);
        soundInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        soundInstance.setParameterByName("SWITCH_Connection", 1);
        soundInstance.start();
    }
    public void StopSound()
    {
        soundInstance.setParameterByName("SWITCH_Connection", 0);
    }
}
