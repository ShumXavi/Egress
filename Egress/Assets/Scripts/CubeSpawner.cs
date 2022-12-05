using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject objTospawn;
    public bool Spawned;
    private GameObject objSpawned;
    // Start is called before the first frame update
    void Start()
    {
        
        Spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCam.CButtOn)
        {
            if (!Spawned)
            {
                //Debug.Log("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE has been pressed");
                objSpawned = Instantiate(objTospawn, transform.position, transform.rotation);
                Spawned = true;
                PlayerCam.CButtOn = false;
            }
            else
            {
                Destroy(objSpawned);
                objSpawned = Instantiate(objTospawn, transform.position, transform.rotation);
                PlayerCam.CButtOn = false;
            }
           
        }
    }
}
