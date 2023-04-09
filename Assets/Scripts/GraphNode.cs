using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public List<GraphNode> neighbours;
    public GameObject currentRobot = null;
    public MeshRenderer lightRing;
    public MeshRenderer lightRing2;
    public Material RMat;
    public Material GMat;
    public Material BMat;
    public Material YMat;
    public Material blankMat;
    private MovesManager manager;

    public void RobotDragged(GameObject robot, Vector3 startPosOfNewRobot) {
        if (currentRobot == null)
        {
            GraphNode oldNnode = robot.GetComponent<Robot>().node;
            if (oldNnode)
                oldNnode.RemoveRobot();
            robot.transform.position = transform.position;
            currentRobot = robot;
            robot.GetComponent<Robot>().node = this;
        }
        else
        {
            GameObject oldRobotGo = currentRobot;
            GameObject newRobotGo = robot;
            Robot oldRobot = currentRobot.GetComponent<Robot>();
            Robot newRobot = robot.GetComponent<Robot>();
            GraphNode oldNode = robot.GetComponent<Robot>().node;
            GraphNode newNode = this;
            
            oldRobotGo.transform.position = startPosOfNewRobot;
            oldRobot.node = newRobot.node;
            if (oldNode != null) oldNode.currentRobot = oldRobotGo;
            
            newRobotGo.transform.position = transform.position;
            newRobot.node = newNode;
            currentRobot = robot;
        }
        RefreshLight();
    }

    public void RemoveRobot() {
        currentRobot = null;
        RefreshLight();
    }

    public void RefreshLight() {
        Material choisenMat = blankMat;
        if (currentRobot)
        {
            switch (currentRobot.GetComponent<Robot>().robotype)
            {
                case Robotype.R: choisenMat = RMat; break;
                case Robotype.G: choisenMat = GMat; break;
                case Robotype.B: choisenMat = BMat; break;
                case Robotype.Y: choisenMat = YMat; break;
            }
        }
        lightRing.material = choisenMat;
        lightRing2.material = choisenMat;
    }
}
