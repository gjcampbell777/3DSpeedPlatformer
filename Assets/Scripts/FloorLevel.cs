using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class FloorLevel : MonoBehaviour
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
        script.lives = 3;
    	script.finished = false;
        respawn();
        script.pivot.transform.Rotate(0.0f, 225.0f, 0.0f, Space.Self);
    	levelBuild();

    }

    void Update()
    {

    	if(script.finished == true)
    	{

            escalation++;
            respawn();
            levelBuild();

    	}

        if(script.respawn == true)
        {
            respawn();
            script.lives--;
            script.respawn = false;
        }

        if(script.lives == 0)
        {
            
            script.lives = -1;
            SceneManager.LoadScene("Hub World", LoadSceneMode.Single);
            
        }

    }

    void respawn()
    {

        script.transform.position = new Vector3((50*escalation)+100, 2, (50*escalation)+100);
        script.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

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

                script.transform.position = new Vector3((50*escalation)+100, 2, (50*escalation)+100);
                script.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
    		}

    	}

    	Instantiate(obstacleList[0], new Vector3((50*escalation)+100, 0, (50*escalation)+100), Quaternion.identity);
    	Instantiate(obstacleList[1], new Vector3(-((50*escalation)+100), 0, -((50*escalation)+100)), Quaternion.identity);

        script.finished = false;
    }

}
