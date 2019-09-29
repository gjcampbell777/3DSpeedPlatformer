using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        pivot.parent = player.transform;

        Cursor.visible = !Cursor.visible;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate(){
        float camX = Input.GetAxisRaw("Mouse X") * rotateSpeed;
        player.Rotate(0, camX, 0);

        float camY = Input.GetAxisRaw("Mouse Y") * rotateSpeed;
        if (invertY){
            pivot.Rotate(camY, 0, 0);
        } else {
            pivot.Rotate(-camY, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < (360 + minViewAngle))
        {
            pivot.rotation = Quaternion.Euler(minViewAngle, 0, 0);
        }

        float camZ = Input.GetAxisRaw("Mouse ScrollWheel");

        float angleX = pivot.eulerAngles.x;
        float angleY = player.eulerAngles.y;
        float cameraAngle = transform.eulerAngles.y;

        //if (Mathf.Abs(cameraAngle - angleY) <= 45){ Potential start for camera following similar to banjo/mario64

            Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);
            transform.position = player.position - (rotation * offset);

        //}

        if(transform.position.y < player.position.y){
            transform.position = new Vector3(transform.position.x, player.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(player.position);
    }
}
