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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
