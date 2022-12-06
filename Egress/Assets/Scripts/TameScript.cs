using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TameScript : MonoBehaviour
{
    public AudioSource TameMusic;
    //public bool MusicOn;
    // Start is called before the first frame update
    void Start()
    {
        TameMusic.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*&& bool hasAirPODS*/
        if (!PlayerMove.MusicOn)
        {
            TameMusic.enabled = false;
            
        }
        /*&& bool hasAirPODS*/
        if (PlayerMove.MusicOn)
        {
            TameMusic.enabled = true;
            
        }
    }
}
