using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public List<GraphNode> neighbours;
    public GameObject currentRobot = null;

    public void RobotDragged(GameObject robot) {
        if (currentRobot == null)
        {
            GraphNode oldNnode = robot.GetComponent<Robot>().node;
            if (oldNnode)
                oldNnode.RemoveRobot();
            robot.transform.position = transform.position;
            currentRobot = robot;
            robot.GetComponent<Robot>().node = this;
        }
    }

    public void RemoveRobot() {
        currentRobot = null;
    }
}
