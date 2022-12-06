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
    public AudioSource DoorAudio;

    void Start()
    {
        DoorAudio.enabled = false;
    }
    void Update()
    {
        GoalButton = PlayerCam.GButtOn;
        //if door unlock move to waypoint
        if (PrPlate.IsOn && GoalButton)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[CurrentWaypoint].transform.position, platspeed * Time.deltaTime);
            //movement from current position to waypoint position at var speed
            DoorAudio.enabled = true;
           // StartCoroutine(ExampleCoroutine());
            
            //Debug.Log("DoorAudio " + DoorAudio.enabled);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[CurrentWaypoint + 1].transform.position, 4* platspeed * Time.deltaTime);
            DoorAudio.enabled = false;
            //StartCoroutine(ExampleCoroutine());
            
        }

    }
    /*IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
       // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        DoorAudio.enabled = false;
        //After we have waited 5 seconds print the time again.
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }*/
}
