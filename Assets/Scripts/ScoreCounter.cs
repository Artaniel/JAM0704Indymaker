using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public MovesManager movesManager;
    private int score;
    public ScoreUI scoreUI;

    public void Count() {
        score = 0;
        foreach (GraphNode node in movesManager.graph.GetComponent<GraphField>().allNodes) {
            foreach (GraphNode neirbor in node.neighbours) {
                if (isSynergy(node, neirbor))
                {
                    score++;
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
        scoreUI.RenderScore(score, movesManager.graph.GetComponent<GraphField>().scoreGoal);
    }

    private bool isSynergy(GraphNode node, GraphNode neirbor) {
        return true; // זהול ֳִ
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
