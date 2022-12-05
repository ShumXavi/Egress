using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        door = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            OpenDoor();
        }
    }
    void OpenDoor()
    {

    }
}
