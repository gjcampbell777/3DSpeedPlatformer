using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

	public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
     	isGrounded = grounded(); 
     	Debug.Log(isGrounded);  
    }

    bool grounded()
    {

        RaycastHit hit;

    	if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.0f))
    	{
    		return true;
    	}

    	return false;
    }
}
