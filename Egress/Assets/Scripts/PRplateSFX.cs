using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRplateSFX : MonoBehaviour
{
    public AudioSource exitSRC;
    // Start is called before the first frame update
    void Start()
    {
        exitSRC.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PrPlate.exitAudio)
        {
            exitSRC.enabled = true;
        }
        else
        {
            exitSRC.enabled = false;
        }
    }
}
