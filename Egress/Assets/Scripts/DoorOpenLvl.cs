using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenLvl : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int CurrentWaypoint = 0;

    [SerializeField]
    float platspeed = .5f;
    //speed of platform movement

    public bool GoalButton;

    void Update()
    {
        GoalButton = PlayerCam.GButtOn;
        //if door unlock move to waypoint
        if (PrPlate.IsOn && GoalButton)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[CurrentWaypoint].transform.position, platspeed * Time.deltaTime);
            //movement from current position to waypoint position at var speed

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[CurrentWaypoint + 1].transform.position, 4* platspeed * Time.deltaTime);
        }

    }
}
