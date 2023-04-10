using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUi : MonoBehaviour
{
   
    //������� ������ ������ "UI" � "MainAudio", �������� �� ���� ���� ������
    public void MouseOnSound() // ������ �� ������/��������
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/MenuUI/UI_MouseOn", gameObject);
    }
    public void MouseClickSound() // ���� �� ������/��������
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/MenuUI/UI_Select", gameObject);
    }
    public void MouseDeClickSound() // ���� �� ������/��������
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
