using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRobotScript : MonoBehaviour
{
    public void GrabSound()//робот взят
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Robots/RB_Grab", gameObject);
    }

    public void LandSound()//робот поставлен
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Robots/RB_Land", gameObject);
    }

    public void SwitchSound()//робот вернулся на предыдущую позицию
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Robots/RB_Switch", gameObject);
    }

}
