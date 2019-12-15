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

    private int jump = 0;
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
    }

    void Update()
    {

        float yStore = moveDirection.y;
        //Need to switch to 'raw' when using keyboard
        //moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1.0f);

        if (jump <= 1)
        {
            //moveDirection = moveDirection.normalized * speed; //Remove this line to make running diagonal the fastest standard run
            velocity.x += moveDirection.x * acceleration; 
            velocity.z += moveDirection.z * acceleration;
        } else {
            //moveDirection = (moveDirection.normalized * speed)/4; //Remove this line to make running diagonal the fastest standard run
            velocity.x += (moveDirection.x * acceleration)/4; 
            velocity.z += (moveDirection.z * acceleration)/4;
        }

        moveDirection.y = yStore;

        if (characterController.isGrounded)
        {
            jump = 0;
            moveDirection.y = 0.0f;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                //moveDirection = (moveDirection.normalized * speed/2);
                velocity.x += (moveDirection.x * acceleration)/2; 
                velocity.z += (moveDirection.x * acceleration)/2;
            } else {
                //moveDirection = moveDirection.normalized * speed;
                velocity.x += moveDirection.x * acceleration; 
                velocity.z += moveDirection.z * acceleration;
            }

            if(Input.GetKey(KeyCode.LeftControl))
            {

                characterController.height /= 2;
                capsule.height /= 2;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight/2, transform.localScale.z);

                if(Input.GetKeyDown(KeyCode.LeftControl) && characterController.velocity != new Vector3(0, 0, 0))
                {
                    startTime = Time.time;
                }

                if(startTime + oneSec >= Time.time)
                {
                
                    //moveDirection = (moveDirection.normalized * speed * 2);
                    velocity.x += (moveDirection.x * acceleration)*2; 
                    velocity.z += (moveDirection.z * acceleration)*2;
                
                } else {
                
                    //moveDirection = (moveDirection.normalized * speed/4);
                    velocity.x += (moveDirection.z * acceleration)/4; 
                    velocity.z += (moveDirection.z * acceleration)/4;

                }

            } else {
                moveDirection = moveDirection.normalized * speed;
                characterController.height = controllerHeight;
                capsule.height = capsuleHeight;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight, transform.localScale.z);
            }

                velocity.x = Mathf.SmoothDamp(velocity.x, 0.0f, ref xVelocity, friction);
                velocity.z = Mathf.SmoothDamp(velocity.z, 0.0f, ref zVelocity, friction);

        } else {

                velocity.x = Mathf.SmoothDamp(velocity.x, 0.0f, ref xVelocity, friction*3);
                velocity.z = Mathf.SmoothDamp(velocity.z, 0.0f, ref zVelocity, friction*3);

        }

        if (Input.GetButtonDown("Jump") && jump <= 1)
        {
            moveDirection.y = jumpHeight;
            jump++;
        }

        moveDirection.y += Physics.gravity.y * gravity * Time.deltaTime;
        velocity.y = moveDirection.y;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Move the controller
        characterController.Move(velocity * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        //Need to switch to 'raw' when using keyboard
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
