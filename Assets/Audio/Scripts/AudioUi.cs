using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUi : MonoBehaviour
{
    public Button buttonComp;
    private bool haveButton;
    private bool activity;
    

    private void OnValidate()
    {
        if (gameObject.GetComponent<Button>() == null)
        {
            haveButton = false;
        }
        else
        {
            haveButton = true;
        }
    }
    private void Start()
    {
        if (haveButton)
        {
            buttonComp = GetComponent<Button>();
        }
    }
    private void Update()
    {
        if (haveButton)
        {
            activity = buttonComp.interactable;
        }
    }
    public void MouseOnSound() // курсор на кнопке/слайдере
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/MenuUI/UI_MouseOn", gameObject);
    }
    public void MouseClickSound() // клик по кнопке/слайдеру
    {
        if (haveButton)
        {
            if (activity)
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/MenuUI/UI_Select", gameObject);
                Debug.Log("button active");
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/MenuUI/UI_Disabled", gameObject);
                Debug.Log("button disabled");
            }
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/MenuUI/UI_Select", gameObject);
            Debug.Log("nobutton");
        }
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
