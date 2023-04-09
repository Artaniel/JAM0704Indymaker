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
}
