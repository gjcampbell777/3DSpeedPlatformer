using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class WaypointRace : MonoBehaviour
{

	public int size = 1;
    public int level = 1;
    public int completionTime = 120;
    public float countdown = 0;
	public GameObject player;
	public PlayerController script;
	public GameObject waypoint;
	public Waypoint groundedScript;
	public GameObject[] obstacleList;

	private int selection = 0;

	RaycastHit hit;
    
    void Awake()
    {
        script.respawnTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {

    	script = player.GetComponent<PlayerController>();
    	size = 2;
    	script.finished = false;
        respawn();
        script.pivot.transform.Rotate(0.0f, 225.0f, 0.0f, Space.Self);
    	levelBuild();
    	waypointPlacement();
    	countdown = Time.time;

    }

    void Update()
    {

    	if(script.finished == true)
    	{

    		waypointPlacement();
            level++;
            if(level > 3)
            {
            	size++;
            	script.respawn = true;
            	script.respawnTime = Time.time + 0.25f;
            	countdown = Time.time;
            	completionTime = 120 * size;
            	levelBuild();
            	level = 1;
            }
            script.finished = false;

    	}

        if(script.respawn == true)
        {
            respawn();
            script.respawn = false;
        }

        if(countdown + completionTime < Time.time)
        {
            
            SceneManager.LoadScene("Hub World", LoadSceneMode.Single);
            
        }

    }

    void respawn()
    {

        script.transform.position = new Vector3(0, 2, 0);

    }

    void levelBuild()
    {

    	GameObject[] oldObjects = GameObject.FindGameObjectsWithTag("Environment");
    	foreach (GameObject target in oldObjects) {
        	GameObject.Destroy(target);
    	}

        oldObjects = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject target in oldObjects) {
            GameObject.Destroy(target);
        }

    	for(int i = 0; i < size; i++){
    		for(int j = 0; j < size; j++){

    			selection = Random.Range(0, obstacleList.Length);
                if(Random.Range(0, 5) == 0 || (i == 0 && j == 0 && i == j))
                {
                    Instantiate(obstacleList[0], new Vector3(100*i+50, 0, 100*j+50),  Quaternion.identity);
                } else {
                	Instantiate(obstacleList[selection], new Vector3(100*i+50, 0, 100*j+50),  Quaternion.Euler(0, 90+Random.Range(0,4), 0));
                }

                selection = Random.Range(0, obstacleList.Length);
                if(Random.Range(0, 5) == 0 || (i == 0 && j == 0 && i == j))
                {
                    Instantiate(obstacleList[0], new Vector3(-100*i-50, 0, -100*j-50),  Quaternion.identity);
                } else {
                	Instantiate(obstacleList[selection], new Vector3(-100*i-50, 0, -100*j-50),  Quaternion.Euler(0, 90+Random.Range(0,4), 0));
                }

                selection = Random.Range(0, obstacleList.Length);
                if(Random.Range(0, 5) == 0 || (i == 0 && j == 0 && i == j))
                {
                    Instantiate(obstacleList[0], new Vector3(100*i+50, 0, -100*j-50),  Quaternion.identity);
                } else {
                	Instantiate(obstacleList[selection], new Vector3(100*i+50, 0, -100*j-50),  Quaternion.Euler(0, 90+Random.Range(0,4), 0));
                }

                selection = Random.Range(0, obstacleList.Length);
                if(Random.Range(0, 5) == 0 || (i == 0 && j == 0 && i == j))
                {
                    Instantiate(obstacleList[0], new Vector3(-100*i-50, 0, 100*j+50),  Quaternion.identity);
                } else {
                	Instantiate(obstacleList[selection], new Vector3(-100*i-50, 0, 100*j+50),  Quaternion.Euler(0, 90+Random.Range(0,4), 0));
                }

    		}

    	} 

    }

    void waypointPlacement()
    {

    	groundedScript.transform.position = new Vector3(
    			Random.Range(-100*size, 100*size), Random.Range(2, 10), Random.Range(-100*size, 100*size));
    }

}
