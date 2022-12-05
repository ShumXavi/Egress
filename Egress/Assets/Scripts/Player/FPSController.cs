using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : PortalTraveller {

    public float walkSpeed = 3;
    public float runSpeed = 6;
    public float smoothMoveTime = 0.1f;
    public float jumpForce = 8;
    public float gravity = 18;
    public float verticalVelocityCap;
    public float warpMultiplier = 1;

    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Vector2 pitchMinMax = new Vector2 (-40, 85);
    public float rotationSmoothTime = 0.1f;

    CharacterController controller;
    Camera cam;
    public float yaw;
    public float pitch;
    float smoothYaw;
    float smoothPitch;

    float yawSmoothV;
    float pitchSmoothV;
    float verticalVelocity;
    public Vector3 velocity;
    public Vector3 warpVelocity;
    Vector3 smoothV;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    private Vector3 targetVelocity;
    bool jumping;
    float lastGroundedTime;
    bool disabled;
    //pickup vars
    public bool CanPickup = false;
    //Death bool
    public static bool isDead = false;

    float pollingTime = 1f;
    private float time;
    private int frameCount;
    

    void Start () {
        cam = Camera.main;
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        controller = GetComponent<CharacterController> ();

        yaw = transform.eulerAngles.y;
        pitch = cam.transform.localEulerAngles.x;
        smoothYaw = yaw;
        smoothPitch = pitch;
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.P)) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Break ();
        }
        if (Input.GetKeyDown (KeyCode.O)) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            disabled = !disabled;
        }

        if (disabled) {
            return;
        }

        Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

        Vector3 inputDir = new Vector3 (input.x, 0, input.y).normalized;
        Vector3 worldInputDir = transform.TransformDirection (inputDir);

        float currentSpeed = (Input.GetKey (KeyCode.LeftShift)) ? runSpeed : walkSpeed;
        targetVelocity = worldInputDir * currentSpeed;
        if (warpVelocity.magnitude > 1)
        {
            velocity = warpVelocity;
            warpVelocity = Vector3.SmoothDamp(warpVelocity, Vector3.zero, ref warpVelocity,smoothMoveTime);
  
            if (controller.isGrounded)
            {
                Debug.Log("Landed");
                warpVelocity = Vector3.zero;
            }
        }
        else
        {
            warpVelocity = Vector3.zero;
            velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref smoothV, smoothMoveTime);
        }
        verticalVelocity -= gravity * Time.deltaTime;


        time += Time.deltaTime;
        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            verticalVelocityCap = verticalVelocityCap % frameRate;
        }

        if(Mathf.Abs(verticalVelocity) >= verticalVelocityCap)
        {
            verticalVelocity = verticalVelocityCap *(-verticalVelocity/verticalVelocity);
        }

        velocity = new Vector3(velocity.x, verticalVelocity, velocity.z);

        if (Input.GetKey(KeyCode.R)){
            Debug.Log(velocity);
        }




        var flags = controller.Move (velocity * Time.deltaTime);
        if (flags == CollisionFlags.Below) {
            jumping = false;
            lastGroundedTime = Time.time;
            verticalVelocity = 0;
        }

        if (Input.GetKeyDown (KeyCode.Space)) {
            float timeSinceLastTouchedGround = Time.time - lastGroundedTime;
            if (controller.isGrounded || (!jumping && timeSinceLastTouchedGround < 0.15f)) {
                jumping = true;
                verticalVelocity = jumpForce;
            }
        }

        float mX = Input.GetAxisRaw ("Mouse X");
        float mY = Input.GetAxisRaw ("Mouse Y");

        // Verrrrrry gross hack to stop camera swinging down at start
        float mMag = Mathf.Sqrt (mX * mX + mY * mY);
        if (mMag > 5) {
            mX = 0;
            mY = 0;
        }

        yaw += mX * mouseSensitivity;
        pitch -= mY * mouseSensitivity;
        pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);
        smoothPitch = Mathf.SmoothDampAngle (smoothPitch, pitch, ref pitchSmoothV, rotationSmoothTime);
        smoothYaw = Mathf.SmoothDampAngle (smoothYaw, yaw, ref yawSmoothV, rotationSmoothTime);

        transform.eulerAngles = Vector3.up * smoothYaw;
        cam.transform.localEulerAngles = Vector3.right * smoothPitch;

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hi");
        //Debug.Log("I am the player I found the trigger1");
        if (other.tag == "Kill")
        {
            isDead = true;
            Debug.Log("I am the player I found the trigger2");
        }

        if (other.tag == "Box")
        {
            CanPickup = true;

        }
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Bye");

        //if (other.gameObject.CompareTag("movingPlatform"))
        //{
        //    Debug.Log("Grape");
        //    transform.SetParent(null);
        //}
    }


    public override void Teleport (Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot) {
        transform.position = pos;
        Vector3 eulerRot = rot.eulerAngles;
        float delta = Mathf.DeltaAngle (smoothYaw, eulerRot.y);
        yaw += delta;
        smoothYaw += delta;
        //transform.eulerAngles = Vector3.up * smoothYaw;
        Debug.Log(toPortal.rotation);
        Debug.Log(fromPortal.rotation);
        warpVelocity = toPortal.TransformVector (fromPortal.InverseTransformVector (velocity)) * warpMultiplier;
        Debug.Log(velocity);
        Debug.Log(warpVelocity);
        velocity = warpVelocity;
        verticalVelocity = warpVelocity.y;

        Physics.SyncTransforms ();
    }

}