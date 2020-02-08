using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class InfinityRunner : MonoBehaviour
{

    public static bool gameOver = false;
	public static int escalation = 1;
    public static int level = 1;
    public static float fullTime;
    public static float distance;
	public GameObject player;
	public PlayerController script;
	public GameObject[] obstacleList;
    public GameObject[] easyObstacleList;
    public GameObject[] medObstaclelIst;
    public GameObject boundaries;

    private int selection;
	private float countdownTime = 3.0f;
	private float spawnTime = 0.0f;

	private Transform lastSpawn;
    
    void Awake()
    {
        script.respawnTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        fullTime = Time.time;
        spawnTime = Time.time;
        gameOver = false;
    	script = player.GetComponent<PlayerController>();
        level = 1;
    	escalation = 1;
        script.lives = 3;
    	script.finished = false;
        script.transform.position = new Vector3(0, 2, 0);
        script.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
        script.pivot.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
    	levelBuild();

    }

    void Update()
    {

    	distance = player.transform.position.z;

    	if(distance > lastSpawn.position.z)
    	{

            level++;
            if(level > 3) 
            {
                escalation++;
                level = 1;
            }

            GameObject spawn;
            GameObject wallOne;
            GameObject wallTwo;

            if(level == 1)
            {

            	if(escalation > 2 && escalation < 5)
            	{
            		selection = Random.Range(0, medObstaclelIst.Length);
            		spawn = Instantiate(medObstaclelIst[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*escalation)-12.5f), Quaternion.identity) as GameObject;
            	} else if(escalation >= 5) {
            		selection = Random.Range(0, obstacleList.Length);
            		spawn = Instantiate(obstacleList[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*escalation)-12.5f), Quaternion.identity) as GameObject;
            	} else {
            		selection = Random.Range(0, easyObstacleList.Length);
            		spawn = Instantiate(easyObstacleList[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*escalation)-12.5f), Quaternion.identity) as GameObject;

            	}

            	wallOne = Instantiate(boundaries, new Vector3(((25f*(escalation)))/2, 0, lastSpawn.transform.position.z+(25*escalation)-12.5f), Quaternion.identity) as GameObject;
            	wallTwo = Instantiate(boundaries, new Vector3(((25f*(escalation)))/-2, 0, lastSpawn.transform.position.z+(25*escalation)-12.5f), Quaternion.identity) as GameObject;

            } else {

            	if(escalation > 2 && escalation < 5)
            	{
            		selection = Random.Range(0, medObstaclelIst.Length);
            		spawn = Instantiate(medObstaclelIst[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*escalation)), Quaternion.identity) as GameObject;
            	} else if(escalation >= 5) {
            		selection = Random.Range(0, obstacleList.Length);
            		spawn = Instantiate(obstacleList[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*escalation)), Quaternion.identity) as GameObject;
            	} else {
            		selection = Random.Range(0, easyObstacleList.Length);
            		spawn = Instantiate(easyObstacleList[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*escalation)), Quaternion.identity) as GameObject;
            	}

            	wallOne = Instantiate(boundaries, new Vector3(((25f*(escalation)))/2, 0, lastSpawn.transform.position.z+(25*escalation)), Quaternion.identity) as GameObject;
            	wallTwo = Instantiate(boundaries, new Vector3(((25f*(escalation)))/-2, 0, lastSpawn.transform.position.z+(25*escalation)), Quaternion.identity) as GameObject;

            }
            
            spawn.transform.localScale = new Vector3(spawn.transform.localScale.x*(0.25f*(escalation)), 1, spawn.transform.localScale.z*(0.25f*escalation));
            wallOne.transform.localScale = new Vector3(wallOne.transform.localScale.x*(0.25f*(escalation)), 1, wallOne.transform.localScale.z*(0.25f*escalation));
            wallTwo.transform.localScale = new Vector3(wallTwo.transform.localScale.x*(0.25f*(escalation)), 1, wallTwo.transform.localScale.z*(0.25f*escalation));

            lastSpawn = spawn.transform;
    	}

    	if(spawnTime + countdownTime < Time.time)
    	{
    		spawnTime = Time.time;

            GameObject[] oldObjects = GameObject.FindGameObjectsWithTag("Environment");
    		for(int i = 0; i < 3; i++) {
        		GameObject.Destroy(oldObjects[i]);
        	}
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
            fullTime = Time.time - fullTime;
            gameOver = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PauseMenu.GameIsOver = true;
            SceneManager.LoadScene("Hub World", LoadSceneMode.Single);
            
        }

    }

    void respawn()
    {

        script.transform.position = new Vector3(0, 10, lastSpawn.transform.position.z);
        script.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

    }

    void levelBuild()
    {

    	GameObject walls;

    	GameObject start = Instantiate(obstacleList[0], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    	start.transform.localScale = new Vector3(25, 1, 25);

    	walls = Instantiate(boundaries, new Vector3(12.5f, 0, 0), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);
        walls = Instantiate(boundaries, new Vector3(-12.5f, 0, 0), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);

        GameObject startOne = Instantiate(obstacleList[0], new Vector3(0, 0, 25),  Quaternion.identity) as GameObject;
        startOne.transform.localScale = new Vector3(25, 1, 25);

        walls = Instantiate(boundaries, new Vector3(12.5f, 0, 25), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);
        walls = Instantiate(boundaries, new Vector3(-12.5f, 0, 25), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);

        GameObject startTwo = Instantiate(obstacleList[0], new Vector3(0, 0, 50),  Quaternion.identity) as GameObject;
        startTwo.transform.localScale = new Vector3(25, 1, 25);

        walls = Instantiate(boundaries, new Vector3(12.5f, 0, 50), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);
        walls = Instantiate(boundaries, new Vector3(-12.5f, 0, 50), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);

    	lastSpawn = startTwo.transform;

        script.finished = false;
    }

}
