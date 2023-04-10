using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundOnMouseover : MonoBehaviour
{
    private bool mouseoverLastFrame;

    //private void Update()
    //{
    //    if (EventSystem.current.IsPointerOverGameObject() && !mouseoverLastFrame)
    //        AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseOnSound();
    //    mouseoverLastFrame = EventSystem.current.IsPointerOverGameObject();
    //}
    public void MenuFilteringOn()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 0);
    }
    public void MenuFilteringOff()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 1);
    }

}
