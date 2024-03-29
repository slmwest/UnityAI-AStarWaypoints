using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    [Header("TamkMovement")]
    Transform goal;
    public float speed = 5.0f;
    public float accuracy = 0.5f;
    public float rotSpeed = 2.5f;

    [Header("TrackerMovement")]
    GameObject tracker;
    public float trackerBoost = 1.1f;
    public int lookAhead = 5;
    float accuracyBoost;

    [Header("Waypoint System")]
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    Node oldLastNode;
    Node oldNextNode;
    int currentWP = 0;
    Graph g;


    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;

        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0]; // TODO: make closest waypoint to tank's starting position
        Time.timeScale = 5; // speed up time by factor of 5!

        // 2 seconds to give enough time to load all waypoints into waypoint manager and graph, etc
        Invoke("GoToRuin", 2f);
    }

    public void GoToHeli()
    {
        Debug.Log("Going to helipad!");
        GoToNewTarget(0);
    }

    public void GoToRuin()
    {
        Debug.Log("Going to ruins!");
        GoToNewTarget(12);
    }

    public void GoToFactory()
    {
        Debug.Log("Going to factory!");
        GoToNewTarget(7);
    }


    public void GoToRockTower()
    {
        Debug.Log("Going to rock tower!");
        GoToNewTarget(15);
    }


    void GoToNewTarget(int targetNodeIdx)
    {
        
        // retain old path in memory temporarily
        if (g.pathList.Count > 1)
        {
            oldLastNode = g.pathList[0];
            oldNextNode = g.pathList[1];
        }
        g.AStar(currentNode, wps[targetNodeIdx]);
        currentWP = 0;

        // if already on correct path, do not go back to start of path but carry on ahead!
        if (g.pathList.Count <= 1)
        {
            return;
        }
        else if (g.pathList[1] == oldNextNode)
        {
            currentWP = 1;
            Debug.Log("Avoided going back to start of path!");
        }
    }

    void ProgressTracker()
    {
        // at goal
        if (currentWP >= g.pathList.Count) { return; }

        // exit if tracker is too far ahead!
        if (Vector3.Distance(tracker.transform.position, this.transform.position) >= lookAhead) { return; }

        // move tracker forward to next waypoint with no interpolation on rotation
        goal = g.pathList[currentWP].getId().transform;
        tracker.transform.LookAt(new Vector3(goal.position.x,
                                            this.transform.position.y, // in case of any inconsistency with y-coords of the invisible WPs!
                                            goal.position.z));
        tracker.transform.Translate(0, 0, (speed * trackerBoost) * Time.deltaTime);

        // progress to next way point if additional waypoints remain
        accuracyBoost = currentWP < g.pathList.Count -1 ? 4 : 1;
        if (Vector3.Distance(tracker.transform.position, g.pathList[currentWP].getId().transform.position) < accuracyBoost * accuracy)
        {
            currentNode = g.pathList[currentWP].getId();
            currentWP++;
            //Debug.Log(currentWP + ", " + (accuracyBoost * accuracy));
        }

    }

    // my attempt at the challenge!
    void LateUpdate()
    {
        // at goal or no goal to follow
        if (g.pathList.Count == 0) { return; }
        if (currentWP >= g.pathList.Count && Vector3.Distance(tracker.transform.position, this.transform.position) < accuracy) { return; }

        ProgressTracker();

        // move tank to follow the tracker
        Quaternion lookAtWP = Quaternion.LookRotation(tracker.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtWP, rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, speed * Time.deltaTime);

    }

}
