using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNodeScript : MonoBehaviour
{
    // ��������� �� ����
   
    FMOD.Studio.EventInstance soundInstance;
    private void Start()
    {
        soundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Nodes/Node_Connection");
        soundInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }


    public void StartSound() // ����� �� ����
    {

        soundInstance.setParameterByName("SWITCH_Connection", 1);
        soundInstance.start();
    }
    public void StopSound() // ����� �� �� ����
    {
        soundInstance.setParameterByName("SWITCH_Connection", 0);
    }
}
