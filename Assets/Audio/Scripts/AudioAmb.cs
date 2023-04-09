using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmb : MonoBehaviour
{
    //поместить на MainAudio, выставить позицию мейнаудио в 0
    FMOD.Studio.EventInstance soundInstance;
    private void Start()
    {
        soundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Amb_Props");
        soundInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        soundInstance.start();
    }
    public void StopSoundEvent()
    {
        soundInstance.release();
    }
}
