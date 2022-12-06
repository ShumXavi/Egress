using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GButtClrChng : MonoBehaviour
{
    public Material[] m_material; 
    Renderer rend;
    public bool HasPlayed;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = m_material[0];
        audioSource.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCam.GButtOn)
        {
            rend.sharedMaterial = m_material[1];
            if (!HasPlayed)
            {
                audioSource.enabled = true;
                HasPlayed = true;
            }
        }
        
    }
}
