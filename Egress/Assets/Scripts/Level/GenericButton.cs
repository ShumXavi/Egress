using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericButton : MonoBehaviour
{
    //this code is to have an object other than the player move between empty waypoint objects
    [SerializeField] GameObject[] waypoints;
    int CurrentWaypoint = 0;

    [SerializeField]
    float buttspeed = 0.25f;
    //speed of button movement
    public bool hasMoved;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (MainCamera.GButtOn && !hasMoved)
        {
            if (Vector3.Distance(transform.position, waypoints[CurrentWaypoint].transform.position) < .01f)
            //this checks if object reaches target waypoint
            {
                CurrentWaypoint++;
                if (CurrentWaypoint >= waypoints.Length)
                //if reached end of waypoint array loop back to first
                {
                    CurrentWaypoint = 0;
                    hasMoved = true;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[CurrentWaypoint].transform.position, buttspeed * Time.deltaTime);
            GetComponent<Key>().open = true;
            //movement from current position to waypoint position at var speed
        }
    }
}
