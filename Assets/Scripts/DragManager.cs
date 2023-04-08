using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragManager : MonoBehaviour
{
    private GameObject selectedRobot = null;
    private bool lastLMBpressed = false;
    public ScoreCounter scoreCounter;

    private void Update()
    {
        if (!lastLMBpressed && Mouse.current.leftButton.value != 0)
            StartDrag();
        if (lastLMBpressed && Mouse.current.leftButton.value == 0)
            EndDrag();
        lastLMBpressed = (Mouse.current.leftButton.value == 1);
    }

    private void StartDrag()
    {
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Mouse.current.position.value), 100f);
        bool robotIsFound = false;
        GameObject foundedRobot = null;
        foreach (RaycastHit hit in hits)
        {
            Robot robot = hit.collider.GetComponent<Robot>();
            if (robot != null) {
                foundedRobot = robot.gameObject;
                robotIsFound = true;
            }
        }
        if (robotIsFound)
        {
            selectedRobot = foundedRobot;
            //надо визуально отобразить что захватили
        }
    }


    private void EndDrag()
    {
        if (selectedRobot)
        {
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Mouse.current.position.value), 100f);
            bool platformIsFound = false;
            GameObject foundedPlatform = null;
            foreach (RaycastHit hit in hits)
            {
                GraphNode platform = hit.collider.GetComponent<GraphNode>();
                if (platform != null)
                {
                    foundedPlatform = platform.gameObject;
                    platformIsFound = true;
                }
            }

            if (platformIsFound)
            {
                foundedPlatform.GetComponent<GraphNode>().RobotDragged(selectedRobot);
                scoreCounter.Count();
                selectedRobot = null;
            }
        }
    }
}
