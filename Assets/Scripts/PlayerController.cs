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

    public float speed;
    public float jumpSpeed;
    public float gravity;
    public float rotationSpeed;
    public Transform pivot;
    public GameObject playerModel;

    private bool extraJump = true;
    private Vector3 moveDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        float yStore = moveDirection.y;
        //Need to switch to 'raw' when using keyboard
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

        if (extraJump == true)
        {
            moveDirection = moveDirection.normalized * speed; //Remove this line to make running diagonal the fastest standard run
        } else
        {
            moveDirection = (moveDirection.normalized * speed)/4; //Remove this line to make running diagonal the fastest standard run
        }

        moveDirection.y = yStore;

        if (characterController.isGrounded)
        {
            extraJump = true;
            moveDirection.y = 0.0f;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection = (moveDirection.normalized * speed/2);
            } else {
                moveDirection = moveDirection.normalized * speed;
            }

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

        } else {
            if (Input.GetButtonDown("Jump") && extraJump == true)
            {
                moveDirection.y = jumpSpeed;
                extraJump = false;
            }

        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravity * Time.deltaTime);

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
