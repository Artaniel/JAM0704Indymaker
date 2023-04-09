using UnityEngine;
using UnityEngine.InputSystem;

public class DragManager : MonoBehaviour
{
    public ScoreCounter scoreCounter;
    public float liftAboveGround = 0.8f;
    
    private GameObject selectedRobot = null;
    private static Vector3 startPosition;
    private bool isReadyToSaveStartPos = true;
    private bool isButtonFree = true;


    private void Update()
    {
        if (selectedRobot == null && Mouse.current.leftButton.isPressed && isButtonFree)
            StartDrag();
        if (selectedRobot != null)
            Dragging(selectedRobot);
        if (!Mouse.current.leftButton.isPressed)
        {
            EndDrag();
            isButtonFree = true;
        }
    }

    private void StartDrag()
    {
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Mouse.current.position.value), 100f);
        bool robotIsFound = false;
        isButtonFree = false;
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
            if (isReadyToSaveStartPos)
            {
                startPosition = selectedRobot.transform.position;
                isReadyToSaveStartPos = false;
            }

            SoundRobotScript soundRobotScript = selectedRobot.GetComponent<SoundRobotScript>();
            if (soundRobotScript)
                soundRobotScript.GrabSound();
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
                foundedPlatform.GetComponent<GraphNode>().RobotDragged(selectedRobot, startPosition);
                scoreCounter.Count();

                SoundRobotScript soundRobotScript = selectedRobot.GetComponent<SoundRobotScript>();
                if (soundRobotScript)
                    soundRobotScript.LandSound();

                selectedRobot = null;
                isReadyToSaveStartPos = true;
            }
            else
            {
                ReturnObjAtStartPos(selectedRobot);

                SoundRobotScript soundRobotScript = selectedRobot.GetComponent<SoundRobotScript>();
                if (soundRobotScript)
                    soundRobotScript.SwitchSound();

                selectedRobot = null;
                isReadyToSaveStartPos = true;

            }            
        }
    }
    
    private void Dragging(GameObject obj)
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Camera.main.WorldToScreenPoint(obj.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        obj.transform.position = new Vector3(worldPosition.x, liftAboveGround, worldPosition.z);
    }
    
    public static void ReturnObjAtStartPos(GameObject obj)
    {
        obj.transform.position = startPosition;
    }

    
}
