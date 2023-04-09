using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundOnMouseover : MonoBehaviour
{
    private bool mouseoverLastFrame;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() && !mouseoverLastFrame)
            AudioSceneTransition.instance?.GetComponent<AudioUi>().MouseOnSound();
        mouseoverLastFrame = EventSystem.current.IsPointerOverGameObject();
    }
}
