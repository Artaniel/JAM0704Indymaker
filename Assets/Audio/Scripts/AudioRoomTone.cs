using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRoomTone : MonoBehaviour
{
    //��������� �� ������ ������
    FMOD.Studio.EventInstance soundInstance;
    

    void Start()
    {
        
        soundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Amb_Props");
        soundInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        soundInstance.start();
    }
    private void Update()
    {
        float yRotation = Mathf.Abs(gameObject.transform.rotation.y);
        soundInstance.setParameterByName("SWITCH_Connection", yRotation);
    }


}