using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphField : MonoBehaviour
{
    public List<GraphLink> allLinks = new List<GraphLink>();
    private List<GraphNode> allNodes;
    public List<GameObject> spawnList;

    void Start()
    {
        InitializeNodes();
        InitialazeLinks();
        DrawAllLinks();
    }

    //заполняет список всех узлов графа
    public void InitializeNodes()
    {
        GraphNode[] nodes = GetComponentsInChildren<GraphNode>();
        allNodes = nodes.ToList();
    }

    //заполняет список всех связей узлов графа
    public void InitialazeLinks()
    {
        foreach (var node in allNodes)
        {
            foreach (var neighbour in node.neighbours)
            {
                AddLink(node, neighbour);
            }
        }
        GetDistinctLinks();
    }
    
    //убирает из списка связей повторяющиеся
    private void GetDistinctLinks()
    {
        for (int i = 0; i < allLinks.Count; i++)
        {
            for (int j = 0; j < allLinks.Count; j++)
            {
                if (allLinks[i].startNode == allLinks[j].endNode && allLinks[j].startNode == allLinks[i].endNode)
                {
                    allLinks.Remove(allLinks[j]);
                }
            }
        }
    }
    
    //создает объект - связь между двумя узлами
    public void AddLink(GraphNode startNode, GraphNode endNode)
    {
        GraphLink link = new GraphLink(startNode, endNode);
        allLinks.Add(link);
    }

    //отрисовывает все связи между узлами
    public void DrawAllLinks()
    {
        foreach (var link in allLinks)
        {
            DrawLinkBetweenTwoNodes(link.startNode.transform, link.endNode.transform);
        }
    }
    
    //отрисовывает одну связь между двумя конкретными узлами
    public void DrawLinkBetweenTwoNodes(Transform startPos, Transform endPos)
    {
        //For creating line renderer object
        LineRenderer lineRenderer = new GameObject("Line" + startPos.gameObject.name + endPos.gameObject.name).
            AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true; 
        lineRenderer.transform.SetParent(gameObject.transform);
                
        //For drawing line in the world space
        lineRenderer.SetPosition(0, startPos.position); 
        lineRenderer.SetPosition(1, endPos.position);
    }

  
}
