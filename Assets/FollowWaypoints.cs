using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class FollowWaypoints : MonoBehaviour
{

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    NavMeshAgent agent;



    // Start is called before the first frame update
    void Start()
    {

        wps = wpManager.GetComponent<WPManager>().waypoints;
        currentNode = wps[0]; // TODO: make closest waypoint to tank's starting position
        Time.timeScale = 5; // speed up time by factor of 5!

        agent = this.GetComponent<NavMeshAgent>();

        // 2 seconds to give enough time to load all waypoints into waypoint manager and graph, etc
        //Invoke("GoToRuin", 2f);
    }

    public void GoToHeli()
    {
        Debug.Log("Going to helipad!");
        agent.SetDestination(wps[0].transform.position);
    }

    public void GoToRuin()
    {
        Debug.Log("Going to ruins!");
        agent.SetDestination(wps[12].transform.position);
    }

    public void GoToFactory()
    {
        Debug.Log("Going to factory!");

        agent.SetDestination(wps[7].transform.position);
    }


    public void GoToRockTower()
    {
        Debug.Log("Going to rock tower!");
        agent.SetDestination(wps[15].transform.position);
    }

    void LateUpdate()
    {


    }

}
