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
    public float distance;
	public GameObject player;
	public PlayerController script;
	public GameObject[] obstacleList;
    public GameObject[] easyObstacleList;
    public GameObject[] medObstaclelIst;
    public GameObject boundaries;
    public Material[] startMaterials;

    private int selection;
    private int size;
	private float countdownTime = 10.0f;
	private float spawnTime = 0.0f;

	private Transform lastSpawn;

	MeshRenderer mr;
    
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

    	Debug.Log(lastSpawn.position.z);

    	distance = player.transform.position.z;

    	if(!lastSpawn)
    	{
    		lastSpawn = player.transform;
    	}
    	
    	if(distance >= lastSpawn.position.z)
    	{

            level++;
            if(level > 3) 
            {
                escalation++;
                level = 1;

                if(escalation > 5 && countdownTime > 5.0f)
                {
                	countdownTime -= 0.5f;
                }

            }

            GameObject spawn;
            GameObject wallOne;
            GameObject wallTwo;

            if(escalation > 6)
            {
            	size = 6;
            } else {
            	size = escalation;
            }

            if(level == 1)
            {

            	if(escalation > 3 && escalation < 6)
            	{
            		selection = Random.Range(0, medObstaclelIst.Length);
            		spawn = Instantiate(medObstaclelIst[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*size)-12.5f), Quaternion.identity) as GameObject;
            	} else if(escalation >= 6) {
            		selection = Random.Range(0, obstacleList.Length);
            		spawn = Instantiate(obstacleList[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*size)-12.5f), Quaternion.identity) as GameObject;
            	} else {
            		selection = Random.Range(0, easyObstacleList.Length);
            		spawn = Instantiate(easyObstacleList[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*size)-12.5f), Quaternion.identity) as GameObject;

            	}

            	wallOne = Instantiate(boundaries, new Vector3(((25f*(size)))/2, 0, lastSpawn.transform.position.z+(25*size)-12.5f), Quaternion.identity) as GameObject;
            	wallTwo = Instantiate(boundaries, new Vector3(((25f*(size)))/-2, 0, lastSpawn.transform.position.z+(25*size)-12.5f), Quaternion.identity) as GameObject;

            } else {

            	if(escalation > 3 && escalation < 6)
            	{
            		selection = Random.Range(0, medObstaclelIst.Length);
            		spawn = Instantiate(medObstaclelIst[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*size)), Quaternion.identity) as GameObject;
            	} else if(escalation >= 6) {
            		selection = Random.Range(0, obstacleList.Length);
            		spawn = Instantiate(obstacleList[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*size)), Quaternion.identity) as GameObject;
            	} else {
            		selection = Random.Range(0, easyObstacleList.Length);
            		spawn = Instantiate(easyObstacleList[selection], new Vector3(0, 0, lastSpawn.transform.position.z+(25*size)), Quaternion.identity) as GameObject;
            	}

            	wallOne = Instantiate(boundaries, new Vector3(((25f*(size)))/2, 0, lastSpawn.transform.position.z+(25*size)), Quaternion.identity) as GameObject;
            	wallTwo = Instantiate(boundaries, new Vector3(((25f*(size)))/-2, 0, lastSpawn.transform.position.z+(25*size)), Quaternion.identity) as GameObject;

            }
            
            spawn.transform.localScale = new Vector3(spawn.transform.localScale.x*(0.25f*(size)), 1, spawn.transform.localScale.z*(0.25f*size));
            wallOne.transform.localScale = new Vector3(wallOne.transform.localScale.x*(0.25f*(size)), 1, wallOne.transform.localScale.z*(0.25f*size));
            wallTwo.transform.localScale = new Vector3(wallTwo.transform.localScale.x*(0.25f*(size)), 1, wallTwo.transform.localScale.z*(0.25f*size));


            lastSpawn = spawn.transform;
            
    	}

    	GameObject[] oldObjects = GameObject.FindGameObjectsWithTag("Environment");

    	if(spawnTime + countdownTime < Time.time)
    	{
    		spawnTime = Time.time;

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

        if(script.lives <= 0)
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

    	GameObject spawn;
        GameObject wallOne;
        GameObject wallTwo;

        if(escalation > 6)
        {
        	size = 6;
        } else {
        	size = escalation;
        }

    	for(int i = 0; i < 3; i++)
    	{
    		spawn = Instantiate(obstacleList[0], new Vector3(0, 0, lastSpawn.transform.position.z+(25*size)), Quaternion.identity) as GameObject;

	    	wallOne = Instantiate(boundaries, new Vector3(((25f*(size)))/2, 0, lastSpawn.transform.position.z+(25*size)), Quaternion.identity) as GameObject;
	    	wallTwo = Instantiate(boundaries, new Vector3(((25f*(size)))/-2, 0, lastSpawn.transform.position.z+(25*size)), Quaternion.identity) as GameObject;
	        
	        spawn.transform.localScale = new Vector3(spawn.transform.localScale.x*(0.25f*(size)), 1, spawn.transform.localScale.z*(0.25f*size));
	        wallOne.transform.localScale = new Vector3(wallOne.transform.localScale.x*(0.25f*(size)), 1, wallOne.transform.localScale.z*(0.25f*size));
	        wallTwo.transform.localScale = new Vector3(wallTwo.transform.localScale.x*(0.25f*(size)), 1, wallTwo.transform.localScale.z*(0.25f*size));


	        lastSpawn = spawn.transform;
    	}

		script.transform.position = new Vector3(0, 11, lastSpawn.position.z+1);
    	script.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

    }

    void levelBuild()
    {

    	GameObject walls;

    	GameObject start = Instantiate(obstacleList[0], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    	start.transform.localScale = new Vector3(25, 1, 25);

    	mr = start.GetComponent<MeshRenderer>();
        mr.material = startMaterials[0];

    	walls = Instantiate(boundaries, new Vector3(12.5f, 0, 0), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);

        mr = walls.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mr.material = startMaterials[1];

        walls = Instantiate(boundaries, new Vector3(-12.5f, 0, 0), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);

        mr = walls.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mr.material = startMaterials[1];

        GameObject startOne = Instantiate(obstacleList[0], new Vector3(0, 0, 25),  Quaternion.identity) as GameObject;
        startOne.transform.localScale = new Vector3(25, 1, 25);

        mr = startOne.GetComponent<MeshRenderer>();
        mr.material = startMaterials[0];

        walls = Instantiate(boundaries, new Vector3(12.5f, 0, 25), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);

        mr = walls.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mr.material = startMaterials[2];

        walls = Instantiate(boundaries, new Vector3(-12.5f, 0, 25), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);

        mr = walls.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mr.material = startMaterials[2];

        GameObject startTwo = Instantiate(obstacleList[0], new Vector3(0, 0, 50),  Quaternion.identity) as GameObject;
        startTwo.transform.localScale = new Vector3(25, 1, 25);

        mr = startTwo.GetComponent<MeshRenderer>();
        mr.material = startMaterials[0];

        walls = Instantiate(boundaries, new Vector3(12.5f, 0, 50), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);

        mr = walls.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mr.material = startMaterials[1];

        walls = Instantiate(boundaries, new Vector3(-12.5f, 0, 50), Quaternion.identity) as GameObject;
        walls.transform.localScale = new Vector3(0.25f, 1, 0.25f);

        mr = walls.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mr.material = startMaterials[1];

    	lastSpawn = startTwo.transform;

        script.finished = false;
    }

}
