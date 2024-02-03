using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// a struct is a value type that can contain fields, properties, and methods
// as opposed to classes, structs are often used for lightweight objects (+ with less memory overhead), immutable data
[System.Serializable]
public struct Link
{
    public enum direction { UNI, BI }
    public direction dir;
    public GameObject node1;
    public GameObject node2;

}

public class WPManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Length > 0)
        {
            foreach(GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }

            foreach (Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI)
                {
                    graph.AddEdge(l.node2, l.node1);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
