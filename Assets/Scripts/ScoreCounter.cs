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

    public void Count() {
        Tscore = 0;
        Bscore = 0;
        Mscore = 0;
        Iscore = 0;
        foreach (GraphNode node in movesManager.graph.GetComponent<GraphField>().allNodes) {
            foreach (GraphNode neirbor in node.neighbours) {
                if (isSynergy(node, neirbor))
                {
                    Tscore++;
                    GraphLink link = FindLink(movesManager.graph.GetComponent<GraphField>(), node, neirbor);
                    link.line.startColor = Color.green;
                    link.line.endColor = Color.green;
                }
                else {
                    GraphLink link = FindLink(movesManager.graph.GetComponent<GraphField>(), node, neirbor);
                    link.line.startColor = Color.red;
                    link.line.endColor = Color.red;
                }
            }
        }
        GraphField graph = movesManager.graph.GetComponent<GraphField>();
        scoreUI.RenderScore(Tscore, Bscore, Mscore, Iscore, graph.Tgoal, graph.Bgoal, graph.Mgoal, graph.Igoal);
    }

    private bool isSynergy(GraphNode node, GraphNode neirbor) {
        //node.currentRobot.GetComponent<Robot>().
        return true; 
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
}
