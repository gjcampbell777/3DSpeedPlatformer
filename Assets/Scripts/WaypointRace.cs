using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]

public class WaypointRace : MonoBehaviour
{

	public static bool gameOver = false;
	public static int size = 1;
    public static int level = 1;
    public static float fullTime;
    public static int completionTime = 120;
    public static float countdown = 0;
	public GameObject player;
	public PlayerController script;
	public GameObject waypoint;
	public Waypoint groundedScript;
	public GameObject[] obstacleList;
	public GameObject boundaries;
	public GameObject WarningText;

	private int selection = 0;

	RaycastHit hit;
	Vector3 lastLocation;
	float wanringCountdown;
    
    void Awake()
    {
        script.respawnTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {

    	lastLocation = new Vector3(0,0,0);
    	fullTime = Time.time;
    	gameOver = false;
    	script = player.GetComponent<PlayerController>();
    	size = 2;
    	level = 1;
    	script.lives = 3;
    	script.finished = false;
        respawn();
        script.pivot.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
    	levelBuild();
    	waypointPlacement();
    	countdown = Time.time;
    	completionTime = 60;
    	WarningText.SetActive(false);

    }

    void Update()
    {

    	if(wanringCountdown + 5.0f < Time.time)
    	{
    		WarningText.SetActive(false);
    	}

    	if(script.finished == true)
    	{

    		lastLocation = groundedScript.transform.position;
    		waypointPlacement();
    		script.lives = 3;
            level++;
            completionTime += 15*(size-1);
            if(level > 3)
            {
            	size++;
            	level = 1;
            	script.lives = 4;
            	script.respawn = true;
            	script.respawnTime = Time.time + 0.25f;
            	countdown = Time.time;
            	completionTime = 60 * (size - 1);
            	levelBuild();
            }
            script.finished = false;

    	}

        if(script.respawn == true)
        {
            respawn();
            script.lives--;
            script.respawn = false;
        }

        if(script.lives == 0)
        {
            
            script.lives = 3;
            lastLocation = groundedScript.transform.position;
            waypointPlacement();
            
        }

        if(countdown + completionTime < Time.time)
        {
            
            gameOver = true;
            fullTime = Time.time - fullTime;
            Cursor.visible = true;
        	Cursor.lockState = CursorLockMode.None;
        	PauseMenu.GameIsOver = true;
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

                if(i == size-1)
                {
                	Instantiate(boundaries, new Vector3(100*(i+1), 0, 100*j+50),  Quaternion.identity);
                	Instantiate(boundaries, new Vector3(-100*(i+1), 0, 100*j+50),  Quaternion.identity);
                	Instantiate(boundaries, new Vector3(100*(i+1), 0, -100*j-50),  Quaternion.identity);
                	Instantiate(boundaries, new Vector3(-100*(i+1), 0, -100*j-50),  Quaternion.identity);
                } 

                if(j == size-1)
                {
                	Instantiate(boundaries, new Vector3(100*i+50, 0, 100*(j+1)),  Quaternion.Euler(0, 90, 0));
                	Instantiate(boundaries, new Vector3(-100*i-50, 0, 100*(j+1)),  Quaternion.Euler(0, 90, 0));
                	Instantiate(boundaries, new Vector3(100*i+50, 0, -100*(j+1)),  Quaternion.Euler(0, 90, 0));
                	Instantiate(boundaries, new Vector3(-100*i-50, 0, -100*(j+1)),  Quaternion.Euler(0, 90, 0));
                }

    		}

    	}



    }

    void waypointPlacement()
    {

    	script.lives = 3;

    	WarningText.SetActive(true);
    	wanringCountdown = Time.time;

    	groundedScript.transform.position = new Vector3(
    			Random.Range(-100*size, 100*size), Random.Range(2, 10), Random.Range(-100*size, 100*size));

    	if((groundedScript.transform.position.x < 100 && groundedScript.transform.position.x > -100)
    		|| (groundedScript.transform.position.z < 100 && groundedScript.transform.position.z > -100))
    	{
    		waypointPlacement();
    	}

    	if((groundedScript.transform.position.x < lastLocation.x+50 && groundedScript.transform.position.x > lastLocation.x-50
    			&& groundedScript.transform.position.z < lastLocation.z+50 && groundedScript.transform.position.z > lastLocation.z-50))
    	{
    		waypointPlacement();
    	}
    }

}
