using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] Waypoint[] nextWaypoints;

    LineRenderer line;


    // Start is called before the first frame update
    void Start()
    {
        line = FindObjectOfType<LineRenderer>();
        DrawLinesToNextWaypoints();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public Waypoint[] getNextWaypoint()
    {
        return nextWaypoints;
    }

    private void DrawLinesToNextWaypoints()
    {

        

        for (int i = 0; i < nextWaypoints.Length; i++)
        {
            
            
            line = Instantiate(line);



            line.startColor = Color.red;
            line.endColor = Color.black;

            line.SetPosition(0, transform.position);
            line.SetPosition(1, nextWaypoints[i].transform.position);




        }
    }
}
