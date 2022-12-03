using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPhysObject : PortalTraveller
{
    public Rigidbody rb;
    public float warpMultiplier = 3;
    public float gravity = 18;
    public bool held;

    void Awake()
    {
        held = false;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!held) {
            rb.AddForce(new Vector3(0, -gravity * Time.deltaTime, 0), ForceMode.Impulse);
        }
        
    }

    public override void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        if (!held)
        {
            base.Teleport(fromPortal, toPortal, pos, rot);
            rb.velocity = toPortal.TransformVector(fromPortal.InverseTransformVector(rb.velocity)) * warpMultiplier;
            rb.angularVelocity = toPortal.TransformVector(fromPortal.InverseTransformVector(rb.angularVelocity)) * warpMultiplier;
        }

    }
}
