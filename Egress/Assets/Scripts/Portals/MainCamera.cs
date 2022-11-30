// Modified from Porals by Sebastian Lague (MIT License)
// https://github.com/SebLague/Portals
// original file: Portals/blob/master/Assets/Scripts/Core/MainCamera.cs

using UnityEngine;

public class MainCamera : MonoBehaviour {

    Portal[] portals;
    [Header("Pickup Settings")]
    [SerializeField] Transform PickupRange;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;

    [SerializeField]
    private float pickupForce = 150.0f;

    void Awake () {
        portals = FindObjectsOfType<Portal> ();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                //Added chack to see if object hit by raycast was a box to prevent player from picking up unintended objects ~ XS
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange) && hit.transform.CompareTag("Box"))
                {
                    //pickup object
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if (heldObj != null)
        {
            MoveObject();
        }
    }
    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Debug.Log("Do RB thing");
            if (pickObj.GetComponent<PortalPhysObject>())
            {
                Debug.Log("Do Phys thing");
                PortalPhysObject phys = pickObj.GetComponent<PortalPhysObject>();
                phys.held = true;
            }
            else
            {
                Debug.Log("Whoops");
            }
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldObjRB.transform.parent = PickupRange;
            heldObj = pickObj;
        }
    }
    void DropObject()
    {
        if (heldObj.GetComponent<PortalPhysObject>())
        {
            Debug.Log("Do Phys thing");
            PortalPhysObject phys = heldObj.GetComponent<PortalPhysObject>();
            phys.held = false;
        }
        {
            heldObjRB.useGravity = true;
            heldObjRB.drag = 1;
            heldObjRB.constraints = RigidbodyConstraints.None;
            heldObjRB.transform.parent = null;
            heldObj = null;
        }
    }
    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, PickupRange.position) > 0.1f)
        {
            Vector3 moveDirection = (PickupRange.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }
    void OnPreCull () {

        for (int i = 0; i < portals.Length; i++) {
            portals[i].PrePortalRender ();
        }
        for (int i = 0; i < portals.Length; i++) {
            portals[i].Render ();
        }

        for (int i = 0; i < portals.Length; i++) {
            portals[i].PostPortalRender ();
        }

    }

}
