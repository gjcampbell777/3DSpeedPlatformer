using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public bool useOffsetValues;
    public bool invertY = false;
    public float rotateSpeed = 1.0f;
    public float maxViewAngle;
    public float minViewAngle;
    public Transform player;
    public Transform pivot;
    public Vector3 offset;

    void Start () {
        if(!useOffsetValues){
            offset = player.position - transform.position;
        }

        pivot.position = player.position;
        pivot.parent = null;

    }

    void LateUpdate(){

    	float camX = 0;
        float camY = 0;

    	if(!PauseMenu.GameIsPaused)
    	{
    		camX = Input.GetAxisRaw("Mouse X") * rotateSpeed;
        	camY = Input.GetAxisRaw("Mouse Y") * rotateSpeed;
    	}
        
        pivot.Rotate(0, camX, 0);
        
        if (invertY){
            pivot.Rotate(camY, 0, 0);
        } else {
            pivot.Rotate(-camY, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.z);
        }

        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < (360 + minViewAngle))
        {
            pivot.rotation = Quaternion.Euler(minViewAngle, pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.z);
        }

        float camZ = Input.GetAxisRaw("Mouse ScrollWheel");
        float angleY = pivot.eulerAngles.y;
        float angleX = pivot.eulerAngles.x;

    	float cameraAngle = transform.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);
        transform.position = player.position - (rotation * offset);

        if(transform.position.y < player.position.y){
            transform.position = new Vector3(transform.position.x, player.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(player.position);
    }
}
