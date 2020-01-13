using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class FloorLevel : MonoBehaviour
{

	public int escalation = 1;
    public int level = 1;
	public GameObject player;
	public PlayerController script;
	public GameObject[] obstacleList;

	private int selection = 0;
    private int height = 2;

    void Awake()
    {
        script.respawnTime = Time.time;
    }

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

            level++;
            if(level > 3) escalation++;
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

        script.transform.position = new Vector3((100*escalation)+50, 2, (100*escalation)+50);
        script.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

    }

    void levelBuild()
    {

    	GameObject[] oldObjects = GameObject.FindGameObjectsWithTag("Environment");
    	foreach (GameObject target in oldObjects) {
        	GameObject.Destroy(target);
    	}

    	Destroy(GameObject.FindWithTag("Finish"));

    	for(int i = 0; i < escalation; i++){
    		for(int j = 0; j < escalation; j++){

                if(Random.Range(0, 20) != 0 || (i == (escalation-1) && j == (escalation-1) && i == j))
                {
                    height = Random.Range(0, 7);
                    selection = Random.Range(2, obstacleList.Length);
                    Instantiate(obstacleList[selection], new Vector3(100*i+50, height, 100*j+50), Quaternion.identity);
                }

                if(Random.Range(0, 20) != 0 || (i == (escalation-1) && j == (escalation-1) && i == j))
                {
                    height = Random.Range(0, 7);
                    selection = Random.Range(2, obstacleList.Length);
                    Instantiate(obstacleList[selection], new Vector3(-100*i-50, height, -100*j-50), Quaternion.identity);
                }

                if(Random.Range(0, 10) != 0)
                {
                    height = Random.Range(0, 7);
                    selection = Random.Range(2, obstacleList.Length);
                    Instantiate(obstacleList[selection], new Vector3(-100*i-50, height, 100*j+50), Quaternion.identity);
                }

                if(Random.Range(0, 10) != 0)
                {
                    height = Random.Range(0, 7);
                    selection = Random.Range(2, obstacleList.Length);
                    Instantiate(obstacleList[selection], new Vector3(100*i+50, height, -100*j-50), Quaternion.identity);
                }

    		}

    	}

    	Instantiate(obstacleList[0], new Vector3((100*escalation)+50, 0, (100*escalation)+50), Quaternion.identity);
    	Instantiate(obstacleList[1], new Vector3(-((100*escalation)+50), 0, -((100*escalation)+50)), Quaternion.identity);

        script.finished = false;
    }

}
