using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRobotScript : MonoBehaviour
{
    public void GrabSound()//����� ����
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Robots/RB_Grab", gameObject);
    }

    public void LandSound()//����� ���������
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Robots/RB_Land", gameObject);
    }

    public void SwitchSound()//����� �������� �� ���������� �������
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Robots/RB_Switch", gameObject);
    }

}
