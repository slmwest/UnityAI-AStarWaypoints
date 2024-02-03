using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Edge> edgeList = new List<Edge>();
    public Node path = null;
    private GameObject id;

    // attribs related to a star algorithm
    public float f, g, h;
    public Node cameFrom;


    // contsructor method
    public Node(GameObject i)
    {
        id = i;
        //xPos = i.transform.position.x;  // not needed unless wanting to pass in vector3s!
        //yPos = i.transform.position.y;
        //zPos = i.transform.position.z;
        path = null;
    }

    // get private id
    public GameObject getId() { return id; }

}
