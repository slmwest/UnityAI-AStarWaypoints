using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();
    public List<Node> pathList = new List<Node>();

    public Graph() { }

    public void AddNode(GameObject id)
    {
        Node node = new Node(id);
        nodes.Add(node);
    }

    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);

        if (from != null && to != null)
        {
            Edge e = new Edge(from, to);
            edges.Add(e);
            from.edgeList.Add(e);
        }
    }
    Node FindNode(GameObject id)
    {
        foreach (Node n in nodes)
        {
            if (n.getId() == id)
                return n;
        }
        return null;
    }

    public bool AStar(GameObject startId, GameObject endId)
    {

        // validation
        Node start = FindNode(startId);
        Node end = FindNode(endId);
        if (start == null && end == null) { return false; }
        if (startId == endId)
        {
            pathList.Clear();
            pathList.Add(end);
            return true;
        }

        // prepare variables
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        float tentative_g_score = 0;
        bool tentative_is_better;
        start.g = 0;
        start.h = Distance(start, end);
        start.f = start.h;

        open.Add(start);
        while (open.Count > 0)
        {
            int i = LowestF(open);
            Node thisNode = open[i];

            // finish algorithm if reached goal
            if (thisNode.getId() == endId)
            {
                ReconstructPath(start, end);
                return true;
            }

            // if not reached goal, explore this node's neighbours, adding each new one to the open list.
            // tentatively score each neighbour visited in terms of g, and if better then update the path to the neighbour with this path.
            // then update the neighbour's f value by combining this new g with the heuristic h
            open.RemoveAt(i);
            closed.Add(thisNode);
            Node neighbour;
            foreach (Edge e in thisNode.edgeList)
            {
                // if the neighbour is already closed then do not pursue further
                neighbour = e.endNode;
                if (closed.IndexOf(neighbour) > -1) { continue;  }

                // calculate g component of fitness then update open list
                tentative_g_score = thisNode.g + Distance(thisNode, neighbour);
                if (open.IndexOf(neighbour) == -1)
                {
                    // add neighbour to open list if not already in list
                    open.Add(neighbour);
                    tentative_is_better = true;
                } else if (tentative_g_score < neighbour.g)
                {
                    tentative_is_better = true;
                } else
                {
                    tentative_is_better = false;
                }

                if (tentative_is_better)
                {
                    neighbour.cameFrom = thisNode;
                    neighbour.g = tentative_g_score;
                    neighbour.h = Distance(thisNode, end);
                    neighbour.f = neighbour.g + neighbour.h;
                }
            }

        }
        // no open nodes to explore, no path can be found
        return false;
    }

    public void ReconstructPath(Node startId, Node endId)
    {
        pathList.Clear();
        pathList.Add(endId);

        // go backwards up the path
        var p = endId.cameFrom;
        while (p != startId && p != null)
        {
            pathList.Insert(0, p); // insert p at the start of the list, using 0th index
            p = p.cameFrom;
        }
        // complete the path adding in the start node
        pathList.Insert(0, startId);
    }


    float Distance(Node a, Node b)
    {
        // remember squared magnitude is faster to work with!
        return (Vector3.SqrMagnitude(a.getId().transform.position - b.getId().transform.position));
    }

    // find the index of an object in a list with lowest f value
    int LowestF(List<Node> l)
    {
        float lowestf = 0;
        int count = 0;
        int iteratorCount = 0;

        // loop through each node l in list and track lowest fitness
        lowestf = l[0].f;
        for (int i = 1; i < l.Count; i++)
        {
            if (l[i].f < lowestf)
            {
                lowestf = l[i].f;
                iteratorCount = count;  // id of node with lowest f
            }
            count++;
        }
        return iteratorCount;
    }
}

