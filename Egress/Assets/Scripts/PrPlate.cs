using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrPlate : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool IsOn;
    //this makes array of drag and drop mats
    public Material[] p_material;
    //access the renderer to change mats
    Renderer rend2;

    //this code is to have an object other than the player move between empty waypoint objects
    [SerializeField] GameObject[] waypoints;
    //int CurrentWaypoint = 0;
    //public bool hasMoved;

    [SerializeField]
    float prspeed = 0.25f;
    //speed of button movement

    void Start()
    {
        rend2 = GetComponent<Renderer>();
        rend2.enabled = true;
        rend2.sharedMaterial = p_material[0];
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("I am the SOMETHING I found the trigger1");
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
        {
            IsOn = true;
            rend2.sharedMaterial = p_material[1];
            //Debug.Log("I found the trigger2");
            Debug.Log("Bool for PR " + IsOn);


            transform.position = Vector3.MoveTowards(transform.position, waypoints[1].transform.position, prspeed * Time.deltaTime);
            //movement from current position to waypoint position at var speed
        }


    }
    void OnCollisionExit(Collision collision)
    {
        IsOn = false;
        rend2.sharedMaterial = p_material[0];
        Debug.Log("Bool for PR " + IsOn);
       

        transform.position = Vector3.MoveTowards(transform.position, waypoints[0].transform.position, prspeed * Time.deltaTime);
        //movement from current position to waypoint position at var speed
    }
}
