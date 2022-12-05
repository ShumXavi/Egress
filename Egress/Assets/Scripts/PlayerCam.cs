using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Camera Sensitivity")]
    //look sensitivity I like 800
    public float sensX;
    public float sensY;

    //used for player orientation animations (make player face forward)
    public Transform orientation;

    [Header("Pickup Settings")]
    [SerializeField] Transform PickupRange;
    private GameObject heldObj;
    private Rigidbody heldObjRB;
    //button bools
    public static bool CButtOn;
    public static bool CmovButtOn;
    public static bool GButtOn;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;

    [SerializeField] private float pickupForce = 150.0f;

   

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;// will probably have to change this line for the HUD
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse input * change in time * sensitivity
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //gets rotation orientation
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        // start pickup methods
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange) && hit.transform.tag == "Box")
                {
                    //pickup object
                    PickupObject(hit.transform.gameObject);
                }
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange) && hit.transform.tag == "Button")
                {
                    if(hit.collider.name == "GButton")
                    {
                        GButtOn = true;
                        Debug.Log("GButton is now " + GButtOn);
                    }
                    if (hit.collider.name == "CButton")
                    {
                        CButtOn = true;
                        CmovButtOn = true;
                        Debug.Log("CButton is now " + CButtOn);
                        Debug.Log("CmovButton is now " + CmovButtOn);

                    }
                    //hit.collider.gameObject.name
                    //hit.collider.name
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
}
