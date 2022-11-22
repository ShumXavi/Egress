using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpwner : MonoBehaviour
{
    public GameObject objTospawn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE has been pressed");
            Instantiate(objTospawn, transform.position, transform.rotation);
        }
    }
}
