using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//updated OnTriggerEnter method had needless bool in there

/*how to add SFX:
 * Drag and drop SFX onto an object making it audio source
 * check play when awake and loop(normally)
 * DISABLE the sound as prefab is enabled
 * add a script with conditions to enable the sound
 * drag audio source obj as audio source in this case footstepSound 
 */

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    Vector3 moveDirection;
    //Movement SFX 
    public AudioSource footstepSound;
    public static bool MusicOn;

    Rigidbody rb;

    //Jump vars
    public static bool isGrounded = false;
    public float jumpForce;
    public float RigidGravity = 9.8f;
    private Collider col;
    private float distToGround;
    

    //cam vars
    public Transform orientation;
    float horizontalInput;
    float verticalInput;

   
    //Death bool
    public static bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        distToGround = col.bounds.extents.y;
        rb.freezeRotation = true;//locks rigidbody to the orientation

    }

    // Update is called once per frame
    void Update()
    {

        MyInput();//separates input vert and horiz for easier access
        MovePlayer();
        FootstepSFX();
        
        
        if (Input.GetKey(KeyCode.M) && !MusicOn)
        {
            MusicOn = true;
            Debug.Log("MusicOn is now " + MusicOn);
        }
        if (Input.GetKey(KeyCode.N) && MusicOn)
        {
            MusicOn = false;
            Debug.Log("MusicOn is now " + MusicOn);
        }
        //ground check
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        //gravity change player was too floaty
        rb.AddForce(new Vector3(0, -1.0f, 0) * rb.mass * RigidGravity);
    }
    private void MyInput()
    {
        //var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //these get input in WASD based on unity libraries
    }
    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        //makes it so player moved forward in the direction facing and can strafe

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //need to cap speed so players don't go above topspeed in due to drag
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("I am the player I found the trigger1");
        if (other.tag == "Kill")
        {
            isDead = true;
            //Debug.Log("I am the player I found the trigger2");
        }
      
    }
    private void FootstepSFX()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            footstepSound.enabled = true;
            
        }
        else
        {
            footstepSound.enabled = false;
        }
    }
   

}
