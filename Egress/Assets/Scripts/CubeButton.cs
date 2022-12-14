using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    public Material[] c_material;
    Renderer rend1;
    //this code is to have an object other than the player move between empty waypoint objects
    [SerializeField] GameObject[] waypoints;
    int CurrentWaypoint = 0;

    public AudioSource sound;
    public bool hasPlayed;

    [SerializeField]
    float buttspeed = 0.25f;
    //speed of button movement
    public bool hasMoved = false;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        rend1 = GetComponent<Renderer>();
        rend1.enabled = true;
        rend1.sharedMaterial = c_material[0];
        sound.enabled = false;
    }

    void Update()
    {
        if (MainCamera.CmovButtOn && !hasMoved)
        {
            if (!hasPlayed)
            {
                sound.enabled = true;
                hasPlayed = true;
            }

            rend1.sharedMaterial = c_material[1];
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
            //movement from current position to waypoint position at var speed
        }
        if (hasMoved)
        {
            hasMoved = false;
            hasPlayed = false;
            MainCamera.CmovButtOn = false;
            rend1.sharedMaterial = c_material[0];
            sound.enabled = false;
        }
        
        
    }
    
}
