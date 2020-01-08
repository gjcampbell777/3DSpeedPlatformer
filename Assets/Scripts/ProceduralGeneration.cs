using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ProceduralGeneration : MonoBehaviour
{

	public int escalation;
	public GameObject[] obstacleList;

	private int selection = 0;

    // Start is called before the first frame update
    void Start()
    {
    	for(int i = 1; i <= escalation; i++){
    		for(int j = 1; j <= escalation; j++){
	    		selection = Random.Range(1, obstacleList.Length);
	    		Instantiate(obstacleList[selection], new Vector3(50*i, 2, 50*j), Quaternion.identity);
	    		
	    		selection = Random.Range(1, obstacleList.Length);
		        Instantiate(obstacleList[selection], new Vector3(-50*i, 3, -50*j), Quaternion.identity);

	    	    selection = Random.Range(1, obstacleList.Length);
	        	Instantiate(obstacleList[selection], new Vector3(-50*i, 4, 50*j), Quaternion.identity);

	        	selection = Random.Range(1, obstacleList.Length);
	        	Instantiate(obstacleList[selection], new Vector3(50*i, 5, -50*j), Quaternion.identity);
    		}

    	}

    	Instantiate(obstacleList[0], new Vector3((50*escalation)+100, 0, (50*escalation)+100), Quaternion.identity);
    	Instantiate(obstacleList[0], new Vector3(-((50*escalation)+100), 0, -((50*escalation)+100)), Quaternion.identity);
        
    }

}
