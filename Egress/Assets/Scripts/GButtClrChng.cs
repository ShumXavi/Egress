using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GButtClrChng : MonoBehaviour
{
    public Material[] m_material; 
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = m_material[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCamera.GButtOn)
        {
            rend.sharedMaterial = m_material[1];
        }
    }
}
