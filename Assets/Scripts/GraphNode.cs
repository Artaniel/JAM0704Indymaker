using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public List<GraphNode> neighbours;
    bool currentStatePlatform = true;
    public void RobotDragged(GameObject robot) {
        if (robot == true && currentStatePlatform == true)
        {
            robot.transform.position = transform.position;
            currentStatePlatform = false;
        }
        if(currentStatePlatform == false)
        {
            robot = null;
        }
        if(robot.transform.position != transform.position)
        {
            currentStatePlatform = true;
        }
    }
}
