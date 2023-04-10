using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUi : MonoBehaviour
{
   
    //создать пустой объект "UI" в "MainAudio", закинуть на него этот скрипт
    public void MouseOnSound() // курсор на кнопке/слайдере
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/MenuUI/UI_MouseOn", gameObject);
    }
    public void MouseClickSound() // клик по кнопке/слайдеру
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/MenuUI/UI_Select", gameObject);
    }
    public void MouseDeClickSound() // клик по кнопке/слайдеру
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/MenuUI/UI_DeSelect", gameObject);
    }
    public void SnapShot()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 0);
    }
    public void SnapShotOff()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/RTPS_Menu", 1);
    }

}
