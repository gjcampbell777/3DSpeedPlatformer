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
    public float jumpHeight;
    public float gravity;
    public float rotationSpeed;
    public Transform pivot;
    public GameObject playerModel;

    private int jump = 0;
    private float capsuleHeight;
    private float controllerHeight;
    private float transformHeight;
    private Vector3 moveDirection;

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
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

        if (jump <= 1)
        {
            moveDirection = moveDirection.normalized * speed; //Remove this line to make running diagonal the fastest standard run
        } else
        {
            moveDirection = (moveDirection.normalized * speed)/4; //Remove this line to make running diagonal the fastest standard run
        }

        moveDirection.y = yStore;

        if (characterController.isGrounded)
        {
            jump = 0;
            moveDirection.y = 0.0f;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection = (moveDirection.normalized * speed/2);
            } else {
                moveDirection = moveDirection.normalized * speed;
            }

            if(Input.GetKey(KeyCode.LeftControl))
            {
                moveDirection = (moveDirection.normalized * speed/4);
                characterController.height /= 2;
                capsule.height /= 2;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight/2, transform.localScale.z);
            } else {
                moveDirection = moveDirection.normalized * speed;
                characterController.height = controllerHeight;
                capsule.height = capsuleHeight;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight, transform.localScale.z);
            }

        } 

        if (Input.GetButtonDown("Jump") && jump <= 1)
        {
            moveDirection.y = jumpHeight;
            jump++;
        }

        moveDirection.y += Physics.gravity.y * gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

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
