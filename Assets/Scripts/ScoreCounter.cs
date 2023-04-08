using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public MovesManager movesManager;
    private int Tscore = 0;
    private int Bscore = 0;
    private int Mscore = 0;
    private int Iscore = 0;
    public ScoreUI scoreUI;
    private const int synergyAmp = 4;

    private void Start()
    {
        movesManager.OnLevelStarted+= Count;
    }

    public void Count() {
        Tscore = 0;
        Bscore = 0;
        Mscore = 0;
        Iscore = 0;
        foreach (GraphNode node in movesManager.graph.GetComponent<GraphField>().allNodes) {
            if (node.currentRobot)
            {
                Robot robot = node.currentRobot.GetComponent<Robot>();
                Tscore += robot.TSkill;
                Bscore += robot.BSkill;
                Mscore += robot.MSkill;
                Iscore += robot.ISkill;
            }
            foreach (GraphNode neirbor in node.neighbours) {                

                int synergy = SynergyMultiplier(node, neirbor);
                if (synergy > 0)
                {
                    GraphLink link = FindLink(movesManager.graph.GetComponent<GraphField>(), node, neirbor);
                    link.line.startColor = Color.green;
                    link.line.endColor = Color.green;
                }
                else if (synergy < 0)
                {
                    GraphLink link = FindLink(movesManager.graph.GetComponent<GraphField>(), node, neirbor);
                    link.line.startColor = Color.red;
                    link.line.endColor = Color.red;
                }
                else
                {
                    GraphLink link = FindLink(movesManager.graph.GetComponent<GraphField>(), node, neirbor);
                    link.line.startColor = Color.white;
                    link.line.endColor = Color.white;
                }
            }
        }
        GraphField graph = movesManager.graph.GetComponent<GraphField>();
        scoreUI.RenderScore(Tscore, Bscore, Mscore, Iscore, graph.Tgoal, graph.Bgoal, graph.Mgoal, graph.Igoal);
        ReadyCheck();
    }

    private void ReadyCheck() {
        GraphField graph = movesManager.graph.GetComponent<GraphField>();
        if (Tscore >= graph.Tgoal && Bscore >= graph.Bgoal && Mscore >= graph.Mgoal && Iscore >= graph.Igoal)
            movesManager.readyButton.interactable = true;
        else
            movesManager.readyButton.interactable = false;
    }

    private int SynergyMultiplier(GraphNode node, GraphNode neirbor) {
        if (!node.currentRobot || !neirbor.currentRobot)
            return 0;
        Robotype first = node.currentRobot.GetComponent<Robot>().robotype;
        Robotype second = neirbor.currentRobot.GetComponent<Robot>().robotype;
        if (first == Robotype.R)
        {
            if (second == Robotype.G)
            {
                Tscore -= synergyAmp;
                Mscore -= synergyAmp;
                return -1;
            }
            else if (second == Robotype.B)
            {
                Tscore += synergyAmp;
                return 1;
            }
            else if (second == Robotype.Y)
            {
                Bscore += synergyAmp;
                return 1;
            }
        }
        else if (first == Robotype.G)
        {
            if (second == Robotype.R)
            {
                Tscore -= synergyAmp;
                Mscore -= synergyAmp;
                return -1;
            }
            else if (second == Robotype.B)
            {
                Iscore += synergyAmp;
                return 1;
            }
            else if (second == Robotype.Y)
            {
                Mscore += synergyAmp;
                return 1;
            }
        }
        else if (first == Robotype.B)
        {
            if (second == Robotype.Y)
            {
                Iscore -= synergyAmp;
                Bscore -= synergyAmp;
                return -1;
            }
            else if (second == Robotype.G)
            {
                Iscore += synergyAmp;
                return 1;
            }
            else if (second == Robotype.R)
            {
                Tscore += synergyAmp;
                return 1;
            }         
        }
        else if (first == Robotype.Y)
        {
            if (second == Robotype.B)
            {
                Iscore -= synergyAmp;
                Bscore -= synergyAmp;
                return -1;
            }
            else if (second == Robotype.G)
            {
                Mscore += synergyAmp;
                return 1;
            }
            else if (second == Robotype.R)
            {
                Bscore += synergyAmp;
                return 1;
            }
        }
        return 0;
    }

    private GraphLink FindLink(GraphField graph, GraphNode node, GraphNode neirbor) {
        foreach (GraphLink link in graph.allLinks)
        {            
            if ((link.startNode == node && link.endNode == neirbor) || (link.startNode == neirbor && link.endNode == node))
                return link;
        }
        Debug.LogWarning("did not found link");
        return null;
    }

    private void OnDestroy()
    {
        movesManager.OnLevelStarted -= Count;
    }
}