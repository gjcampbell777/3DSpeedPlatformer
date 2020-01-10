using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ProceduralGeneration : MonoBehaviour
{

	public int escalation;
	public GameObject player;
	public PlayerController script;
	public GameObject[] obstacleList;

	private int selection = 0;

    // Start is called before the first frame update
    void Start()
    {

    	script = player.GetComponent<PlayerController>();
    	escalation = 1;
    	script.finished = false;
    	levelBuild();

    }

    void Update()
    {

    	if(script.finished == true)
    	{

    		escalation++;

    		levelBuild();

    	}

    }

    void levelBuild()
    {

    	GameObject[] oldObjects = GameObject.FindGameObjectsWithTag("Environment");
    	foreach (GameObject target in oldObjects) {
        	GameObject.Destroy(target);
    	}

    	Destroy(GameObject.FindWithTag("Finish"));

    	for(int i = 1; i <= escalation; i++){
    		for(int j = 1; j <= escalation; j++){
	    		selection = Random.Range(2, obstacleList.Length);
	    		Instantiate(obstacleList[selection], new Vector3(50*i, 2, 50*j), Quaternion.identity);
	    		
	    		selection = Random.Range(2, obstacleList.Length);
		        Instantiate(obstacleList[selection], new Vector3(-50*i, 3, -50*j), Quaternion.identity);

	    	    selection = Random.Range(2, obstacleList.Length);
	        	Instantiate(obstacleList[selection], new Vector3(-50*i, 4, 50*j), Quaternion.identity);

	        	selection = Random.Range(2, obstacleList.Length);
	        	Instantiate(obstacleList[selection], new Vector3(50*i, 5, -50*j), Quaternion.identity);
    		}

    	}

    	Instantiate(obstacleList[0], new Vector3((50*escalation)+100, 0, (50*escalation)+100), Quaternion.identity);
    	Instantiate(obstacleList[1], new Vector3(-((50*escalation)+100), 0, -((50*escalation)+100)), Quaternion.identity);

    	player.transform.position = new Vector3((50*escalation)+100, 2, (50*escalation)+100);
    	player.transform.Rotate(0.0f, 225.0f, 0.0f, Space.Self);

    	if(escalation == 1)
    	{
    		script.pivot.transform.Rotate(0.0f, 225.0f, 0.0f, Space.Self);
    	}

    	script.finished = false;
    }

}
