using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTraveler : MonoBehaviour
{
    //this code is to have an object other than the player mover between empty waypoint objects
    [SerializeField] GameObject[] waypoints;
    int CurrentWaypoint = 0;

    [SerializeField]
    float platspeed = 1.0f;
    //speed of platform movement
    
    

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, waypoints[CurrentWaypoint].transform.position) < .01f)
            //this checks if object reaches target waypoint
        {
            CurrentWaypoint++;
            if (CurrentWaypoint >= waypoints.Length)
                //if reached end of waypoint array loop back to first
            {
                CurrentWaypoint = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[CurrentWaypoint].transform.position, platspeed * Time.deltaTime);
        //movement from current position to waypoint position at var speed
    }
}
