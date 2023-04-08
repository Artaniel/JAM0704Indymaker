using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public List<GraphNode> neighbours;
    public GameObject currentRobot = null;

    public void RobotDragged(GameObject robot) {
        if (currentRobot == null)
        {
            robot.transform.position = transform.position;
            currentRobot = robot;
        }
    }
}
