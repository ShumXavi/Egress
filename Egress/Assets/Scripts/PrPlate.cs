using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrPlate : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool IsOn;

    void Start()
    {

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
            Debug.Log("I found the trigger2");
            Debug.Log("Bool for PR " + IsOn);
        }
            
        
    }
    void OnCollisionExit(Collision collision)
    {
        IsOn = false;
        Debug.Log("Bool for PR " + IsOn);
    }
}
