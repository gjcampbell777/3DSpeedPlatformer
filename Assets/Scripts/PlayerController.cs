using UnityEngine;
using UnityEngine.SceneManagement;
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

    public bool finished = false;
    public bool respawn = false;
    public int lives = 3;
    public static int shownLives = 3;
    public float maxSpeed;
    public float acceleration;
    public float friction;
    public float jumpHeight;
    public float gravity;
    public float rotationSpeed;
    public Transform pivot;
    public GameObject playerModel;
    public AudioSource jumpNoise;
    public AudioSource respawnNoise;
    public Material[] characterSkins;
    public static int characterSelect = 0;
    public GameObject playerSkin;

    private bool wallRunning = false;
    private bool diving = false;
    private bool sliding = false;
    private int jump = 0;
    private int wall = 0;
    private int dive = 0;
    private float maxSpeedStore;
    private float accelerationStore;
    private float capsuleHeight;
    private float controllerHeight;
    private float transformHeight;
    private float xVelocity = 0.0f;
    private float zVelocity = 0.0f;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private Vector3 gravityVelocity;

    bool jumpPress = false;
    bool grounded = true;
    float slideTime = 0.0f;
    float wallRunTime = 0.0f;
    public float respawnTime = 0.0f;
    float oneSec = 1.0f;
    float halfSec = 0.5f;
    float quarterSec = 0.25f;
    float tenthSec = 0.1f;
    MeshRenderer mr;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        capsule = GetComponent<CapsuleCollider>();
        transformHeight = transform.localScale.y;
        controllerHeight = characterController.height;
        capsuleHeight = capsule.height;
        maxSpeedStore = maxSpeed;
        accelerationStore = acceleration;

        shownLives = 3;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        mr = playerSkin.GetComponent<MeshRenderer>();
        mr.material =  characterSkins[characterSelect];

        //Application.targetFrameRate = 15;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if(hit.gameObject.tag == "Wall")
        {
            wallRunning = true;
            wall++;
        }

        if(hit.gameObject.tag == "Finish")
        {

            velocity = new Vector3(0,0,0);
            moveDirection = new Vector3(0,0,0);
            maxSpeed = 0;

            finished = true;
            respawnTime = Time.time;
            respawnNoise.Play();
        }

        if(hit.gameObject.tag == "Waypoint")
        {

            finished = true;
            respawnTime = Time.time;
            respawnNoise.Play();
            
        }

        if(hit.gameObject.tag == "Obstacle Portal")
        {
            FloorLevel.gameOver = false;
            WaypointRace.gameOver = false;
            InfinityRunner.gameOver = false;
            SceneManager.LoadScene("Floor Level", LoadSceneMode.Single);
            respawnTime = Time.time;
            respawnNoise.Play();
            FloorLevel.gameOver = false;
            WaypointRace.gameOver = false;
            InfinityRunner.gameOver = false;
        }

        if(hit.gameObject.tag == "Waypoint Portal")
        {
            FloorLevel.gameOver = false;
            WaypointRace.gameOver = false;
            InfinityRunner.gameOver = false;
            SceneManager.LoadScene("Waypoint Race", LoadSceneMode.Single);
            respawnTime = Time.time;
            respawnNoise.Play();
            FloorLevel.gameOver = false;
            WaypointRace.gameOver = false;
            InfinityRunner.gameOver = false;
        }

        if(hit.gameObject.tag == "Infinite Portal")
        {
            FloorLevel.gameOver = false;
            WaypointRace.gameOver = false;
            InfinityRunner.gameOver = false;
            SceneManager.LoadScene("Infinity Runner", LoadSceneMode.Single);
            respawnTime = Time.time;
            respawnNoise.Play();
            FloorLevel.gameOver = false;
            WaypointRace.gameOver = false;
            InfinityRunner.gameOver = false;
        }
        
    }

    void Update()
    {

        if (jump >= 2)
        {
            if(maxSpeed > maxSpeedStore/1.5f)
            {
                maxSpeed -= acceleration;
            }
        }

        if (characterController.collisionFlags == CollisionFlags.None)
        {
            wallRunning = false;
            wall = 0;
        }

        if(transform.position.y < -20.0f)
        {

            respawn = true;
            respawnTime = Time.time;
            shownLives--;
            respawnNoise.Play();

            if (SceneManager.GetActiveScene().name == "Hub World")
            {
                transform.position = new Vector3(0, 2, 0);
                transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
            }

        }

        if (characterController.isGrounded)
        {

            grounded = true;
            diving = false;
            dive = 0;
            jump = 0;
            moveDirection.y = 0.0f;

            if(maxSpeed > maxSpeedStore)
            {
                maxSpeed -= acceleration/5;
            }

            if(maxSpeed < maxSpeedStore && !Input.GetButton("Walk") && !Input.GetButton("Crouch"))
            {
                maxSpeed += acceleration;
            }

            if(Input.GetButton("Walk"))
            {
                if(maxSpeed > maxSpeedStore/2)
                {
                    maxSpeed -= acceleration;
                }
            }

            if((Input.GetButton("Crouch") || Input.GetAxis("Crouch") == 1.0f) && !PauseMenu.GameIsPaused)
            {

                characterController.height /= 2;
                capsule.height /= 2;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight/2, transform.localScale.z);

                if(Input.GetButtonDown("Crouch") || Input.GetAxis("Crouch") == 1.0f)
                {
                    if((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !sliding)
                    {
                        slideTime = Time.time;
                        sliding = true;
                    } 
                } 

                if(slideTime + halfSec > Time.time)
                {

                    if(maxSpeed < maxSpeedStore*2)
                    {
                        maxSpeed += acceleration*5;
                    }

                    if(Input.GetButton("Jump") && slideTime + quarterSec >= Time.time)
                    {
                        maxSpeed += 5;
                    }
                
                } else {

                    friction = 0f;

                    if(maxSpeed > maxSpeedStore/4)
                    {
                        maxSpeed -= acceleration*2;
                    }

                }

            } else {

                characterController.height = controllerHeight;
                capsule.height = capsuleHeight;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight, transform.localScale.z);
                sliding = false;

            }

        } else {
            
            slideTime = Time.time;
            sliding = false;
            grounded = false;

            if((Input.GetButton("Crouch") || Input.GetAxis("Crouch") == 1.0f) && !PauseMenu.GameIsPaused)
            {

                characterController.height /= 2;
                capsule.height /= 2;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight/2, transform.localScale.z);
                diving = true;

            } else {

                characterController.height = controllerHeight;
                capsule.height = capsuleHeight;
                transform.localScale = new Vector3(transform.localScale.x, transformHeight, transform.localScale.z);

            }

            //Might want to change move direction value to make triggering this speed up effect easier/harder 
            if(diving && moveDirection.y > 0.75 && (Input.GetButtonDown("Crouch") || Input.GetAxis("Crouch") == 1.0f ))
            {
                dive++;
                
            }

        }

        if (Input.GetButtonDown("Jump") && jump <= 1)
        {
            jumpPress = true;
            jump++;
            jumpNoise.Play();

            if(wallRunning) wallRunning = false;

        }

        if(Input.GetButtonDown("Character Swap"))
        {
            characterSelect = Random.Range(0, characterSkins.Length);
            mr.material =  characterSkins[characterSelect];
        }

        if((Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) || (Input.GetButton("Stop") || Input.GetAxis("Stop") == 1.0f)){

            //Remove or "lower" friction to add an 'ice' effect
            velocity.x = Mathf.SmoothDamp(velocity.x, 0.0f, ref xVelocity, 0.1f);
            velocity.z = Mathf.SmoothDamp(velocity.z, 0.0f, ref zVelocity, 0.1f);
            maxSpeed = 25;

        }

        //Debug.Log(transform.position);
        //Debug.Log(velocity.x + "," + velocity.z);

        //Move the player in different directions based on camera look direction
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

    }

    void FixedUpdate()
    {

        float yStore = moveDirection.y;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1.0f);
        moveDirection.y = yStore;

        if (wallRunning)
        {

            if(maxSpeed < maxSpeedStore*1.5f)
            {
                maxSpeed += acceleration;
            }

            jump = 0;

            if(wall == 1)
            {
                wallRunTime = Time.time;
            }

            if(wallRunTime + oneSec < Time.time)
            {
                
                moveDirection.y += Physics.gravity.y * (gravity/8) * Time.deltaTime;
                
            } else {
                moveDirection.y = 0.0f;
            }

        }

        if(dive == 1){
            maxSpeed += 10;
        }

        moveDirection.y += Physics.gravity.y * gravity * Time.deltaTime;

        velocity.x += moveDirection.x; 
        velocity.z += moveDirection.z;

        if((!grounded || sliding) && !wallRunning)
        {
            friction = 1.75f;
        } else {
            friction = 1f;
        }

        //Remove or "lower" friction to add an 'ice' effect
        velocity.x = Mathf.SmoothDamp(velocity.x, 0.0f, ref xVelocity, friction);
        velocity.z = Mathf.SmoothDamp(velocity.z, 0.0f, ref zVelocity, friction);

        if(characterController.velocity == new Vector3(0, 0, 0))
        {
            maxSpeed = maxSpeedStore;
        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        if(jumpPress) 
        {
            moveDirection.y = jumpHeight;
            jumpPress = false;
        }

        velocity.y = moveDirection.y;

        // Move the controller
        if((finished == false || respawn == false) && respawnTime + tenthSec < Time.time)
        {
            characterController.Move(velocity * Time.deltaTime);
            respawn = false;
        }
        
    }
}
