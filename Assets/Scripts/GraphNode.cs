using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public List<GraphNode> neighbours;

    public void RobotDragged(GameObject robot) {
        robot.transform.position = transform.position;
    }
}
