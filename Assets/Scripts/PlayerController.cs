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

    public float speed = 20.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = 5.0f;

    private bool extraJump = true;
    private Vector3 moveDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));

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
    }
}
