using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    Transform goal;
    public float speed = 5.0f;
    public float accuracy = 1.0f;
    public float roSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[currentWP];

        GoToRuin();
    }

    public void GoToHeli()
    {
        int targetNodeIdx = 0;
        g.AStar(currentNode, wps[targetNodeIdx]);
        currentWP = 0;
    }

    public void GoToRuin()
    {
        int targetNodeIdx = 12;
        g.AStar(currentNode, wps[targetNodeIdx]);
        currentWP = 0;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
