using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    CapsuleCollider capsule;

    public float speed;
    public float maxSpeed;
    public float acceleration;
    public float friction;
    public float jumpHeight;
    public float gravity;
    public float rotationSpeed;
    public Transform pivot;
    public GameObject playerModel;

    private bool wallRunning = false;
    private int jump = 0;
    private int wall = 0;
    private float maxSpeedStore;
    private float capsuleHeight;
    private float controllerHeight;
    private float transformHeight;
    private float xVelocity = 0.0f;
    private float zVelocity = 0.0f;
    private Vector3 moveDirection;
    private Vector3 velocity;

    float startTime = 0.0f;
    float oneSec = 1.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        capsule = GetComponent<CapsuleCollider>();
        transformHeight = transform.localScale.y;
        controllerHeight = characterController.height;
        capsuleHeight = capsule.height;
        maxSpeedStore = maxSpeed;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if(hit.gameObject.tag == "Wall")
        {
            wallRunning = true;
            wall++;
        }
        
    }

    void Update()
    {

        float yStore = moveDirection.y;
        maxSpeed = maxSpeedStore;
        //Need to switch to 'raw' when using keyboard
        //moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1.0f);

        velocity.x += moveDirection.x * acceleration; 
        velocity.z += moveDirection.z * acceleration;

        if (jump <= 1)
        {
            //moveDirection = moveDirection.normalized * speed; //Remove this line to make running diagonal the fastest standard run
            //maxSpeed = maxSpeedStore;

        } else {
            //moveDirection = (moveDirection.normalized * speed)/4; //Remove this line to make running diagonal the fastest standard run
            maxSpeed = maxSpeedStore/2;
        }

        moveDirection.y = yStore;

        if (characterController.isGrounded)
        {
            jump = 0;
            moveDirection.y = 0.0f;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                //moveDirection = (moveDirection.normalized * speed/2);
                maxSpeed = maxSpeedStore/2;
            } else {
                //moveDirection = moveDirection.normalized * speed;
                maxSpeed = maxSpeedStore;
            }

            if(Input.GetKey(KeyCode.LeftControl))
            {

                characterController.height /= 2;
                capsule.height /= 2;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight/2, transform.localScale.z);

                if(Input.GetKeyDown(KeyCode.LeftControl) && characterController.velocity != new Vector3(0, 0, 0)
                    && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
                {
                    startTime = Time.time;
                } 

                if(startTime + oneSec >= Time.time)
                {
                
                    //moveDirection = (moveDirection.normalized * speed * 2);
                    maxSpeed = maxSpeedStore*2;
                
                } else {
                
                    //moveDirection = (moveDirection.normalized * speed/4);
                    maxSpeed = maxSpeedStore/4;

                }

            } else {
                moveDirection = moveDirection.normalized * speed;
                characterController.height = controllerHeight;
                capsule.height = capsuleHeight;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight, transform.localScale.z);
            }

        }

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){

            velocity.x = Mathf.SmoothDamp(velocity.x, 0.0f, ref xVelocity, friction);
            velocity.z = Mathf.SmoothDamp(velocity.z, 0.0f, ref zVelocity, friction);

        }

        if (Input.GetButtonDown("Jump") && jump <= 1)
        {
            moveDirection.y = jumpHeight;
            jump++;
        }

        if (characterController.collisionFlags == CollisionFlags.None)
        {
            wallRunning = false;
            wall = 0;
        }

        moveDirection.y += Physics.gravity.y * gravity * Time.deltaTime;

        if (wallRunning)
        {
            maxSpeed = maxSpeedStore*1.5f;
            jump = 0;

            if(wall == 1)
            {
                startTime = Time.time;
            }

            if(startTime + oneSec < Time.time)
            {
                
                moveDirection.y += Physics.gravity.y * (gravity/8) * Time.deltaTime;
                //wallRunning = false;
                maxSpeed = maxSpeedStore;
                
            } else {
                moveDirection.y = 0.0f;
            }
        } 

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        velocity.y = moveDirection.y;

        // Move the controller
        characterController.Move(velocity * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
