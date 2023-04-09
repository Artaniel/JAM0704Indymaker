using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmb : MonoBehaviour
{
    //поместить на MainAudio, выставить позицию мейнаудио в 0
    private void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Ambient/Amb_Props", gameObject);
    }
}
