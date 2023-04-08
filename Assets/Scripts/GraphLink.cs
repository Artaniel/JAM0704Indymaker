using UnityEngine;

public class GraphLink
{
    public GraphNode startNode;
    public GraphNode endNode;
    public LineRenderer line;

    public GraphLink(GraphNode startNod, GraphNode endNod)
    {
        startNode = startNod;
        endNode = endNod;
    }
}