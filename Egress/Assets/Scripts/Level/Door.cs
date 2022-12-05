using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int CurrentWaypoint = 0;
    [SerializeField]
    float speed = .5f;
    public GameObject door;
    public bool goal;
    // Start is called before the first frame update
    void Start()
    {
        door = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (goal)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }
    void OpenDoor()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[CurrentWaypoint].transform.position, speed * Time.deltaTime);
    }
    void CloseDoor()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[CurrentWaypoint + 1].transform.position, 4 * speed * Time.deltaTime);
    }
}
