using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioRoomTone : MonoBehaviour
{
    //поместить на камера холдер
    public StudioListener cam;
    FMOD.Studio.EventInstance soundInstance;
    
    

    void Start()
    {
        cam = FindObjectOfType<StudioListener>();
        soundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Amb_RoomTone");
        soundInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        soundInstance.start();
    }

    private void Update()
    {
        float yRotation = Mathf.Abs(cam.gameObject.transform.position.z);
        soundInstance.setParameterByName("SWITCH_Connection", yRotation);
    }
    //public void StopSoundEvent()
    //{
    //    soundInstance.release();
    //}

    public static void StopSoundEvent()
    {
        FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Amb_Props").release();
    }


}
