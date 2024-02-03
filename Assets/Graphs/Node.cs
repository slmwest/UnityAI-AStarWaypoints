using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Edge> edgeList = new List<Edge>();
    public Node path = null;
    private GameObject id;
    public float xPos;
    public float yPos;
    public float zPos;

    // a star fields
    public float f, g, h;
    public Node cameFrom;


    // contsructor method
    public Node(GameObject i)
    {
        id = i;
        xPos = i.transform.position.x;
        yPos = i.transform.position.y;
        zPos = i.transform.position.z;
        path = null;
    }

    // get private id
    public GameObject getId() { return id; }

}
