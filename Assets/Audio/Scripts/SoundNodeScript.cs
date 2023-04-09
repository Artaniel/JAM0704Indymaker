using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNodeScript : MonoBehaviour
{
    // поместить на ноду
   
    FMOD.Studio.EventInstance soundInstance;
    private void Start()
    {
        soundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Nodes/Node_Connection");
        soundInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }


    public void StartSound() // робот на ноде
    {

        soundInstance.setParameterByName("SWITCH_Connection", 1);
        soundInstance.start();
    }
    public void StopSound() // робот не на ноде
    {
        soundInstance.setParameterByName("SWITCH_Connection", 0);
    }
}
