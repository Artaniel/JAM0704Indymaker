using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRoomTone : MonoBehaviour
{
    //поместить на камера холдер
    FMOD.Studio.EventInstance soundInstance;
    

    void Start()
    {
        
        soundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Amb_RoomTone");
        soundInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        soundInstance.start();
    }
    private void Update()
    {
        float yRotation = Mathf.Abs(gameObject.transform.rotation.y);
        soundInstance.setParameterByName("SWITCH_Connection", yRotation);
    }

    public void StopSoundEvent()
    {
        soundInstance.release();
    }


}
