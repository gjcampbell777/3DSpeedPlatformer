using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class FloorLevel : MonoBehaviour
{

    public static bool gameOver = false;
	public static int escalation = 1;
    public static int level = 1;
    public static float fullTime;
	public GameObject player;
	public PlayerController script;
	public GameObject[] obstacleList;
    public GameObject[] easyObstacleList;
    public GameObject[] medObstaclelIst;
    public GameObject boundaries;
    public GameObject[] cornerList;

	private int selection = 0;
    
    void Awake()
    {
        script.respawnTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        fullTime = Time.time;
        gameOver = false;
    	script = player.GetComponent<PlayerController>();
        level = 1;
    	escalation = 1;
        script.lives = 3;
    	script.finished = false;
        respawn();
        player.transform.Rotate(0.0f, 220.0f, 0.0f, Space.Self);
        script.pivot.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
    	levelBuild();

    }

    void Update()
    {

    	if(script.finished == true)
    	{

            level++;
            if(level > 3) 
            {
                escalation++;
                level = 1;
            }
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

        script.transform.position = new Vector3((100*escalation)+50, 7, (100*escalation)+50);
        script.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

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

    	Destroy(GameObject.FindWithTag("Finish"));

    	for(int i = 0; i < escalation; i++){
    		for(int j = 0; j < escalation; j++){

                if(Random.Range(0, 10) != 0 || (i == (escalation-1) && j == (escalation-1) && i == j))
                {
                    switch (level)
                    {
                        case 1:
                            selection = Random.Range(0, easyObstacleList.Length);
                            if(i == (escalation-1) && j == (escalation-1) && i == j)
                            {
                                selection = Random.Range(0, cornerList.Length);
                                if(selection == 0 || selection == 7 || selection == 8)
                                {
                                    Instantiate(cornerList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.identity);
                                } else {
                                    Instantiate(cornerList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, 45, 0));
                                }

                            } else {
                                Instantiate(easyObstacleList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                            }
                            break;
                        case 2:
                            selection = Random.Range(0, medObstaclelIst.Length);
                            if(i == (escalation-1) && j == (escalation-1) && i == j)
                            {
                                selection = Random.Range(0, cornerList.Length);
                                if(selection == 0 || selection == 7 || selection == 8)
                                {
                                    Instantiate(cornerList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.identity);
                                } else {
                                    Instantiate(cornerList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, 45, 0));
                                }
                            } else {
                                Instantiate(medObstaclelIst[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                            }
                            break;
                        case 3:
                            selection = Random.Range(2, obstacleList.Length);
                            if(i == (escalation-1) && j == (escalation-1) && i == j)
                            {
                                selection = Random.Range(0, cornerList.Length);
                                if(selection == 0 || selection == 7 || selection == 8)
                                {
                                    Instantiate(cornerList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.identity);
                                } else {
                                    Instantiate(cornerList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, 45, 0));
                                }
                            } else {
                                Instantiate(obstacleList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                            }
                            break;
                        default:
                            selection = Random.Range(2, obstacleList.Length);
                            if(i == (escalation-1) && j == (escalation-1) && i == j)
                            {
                                selection = Random.Range(0, cornerList.Length);
                                if(selection == 0 || selection == 7 || selection == 8)
                                {
                                    Instantiate(cornerList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.identity);
                                } else {
                                    Instantiate(cornerList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, 45, 0));
                                }
                            } else {
                                Instantiate(obstacleList[selection], new Vector3(100*i+50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                            }
                            break;
                    }
                }

                if(Random.Range(0, 10) != 0 || (i == (escalation-1) && j == (escalation-1) && i == j))
                {
                    
                    switch (level)
                    {
                        case 1:
                            selection = Random.Range(0, easyObstacleList.Length);
                            if(i == (escalation-1) && j == (escalation-1) && i == j)
                            {
                                selection = Random.Range(0, cornerList.Length);
                                if(selection == 0 || selection == 7 || selection == 8)
                                {
                                    Instantiate(cornerList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.identity);
                                } else {
                                    Instantiate(cornerList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, 45, 0));
                                }
                            } else {
                                Instantiate(easyObstacleList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                            }
                            break;
                        case 2:
                            selection = Random.Range(0, medObstaclelIst.Length);
                            if(i == (escalation-1) && j == (escalation-1) && i == j)
                            {
                                selection = Random.Range(0, cornerList.Length);
                                if(selection == 0 || selection == 7 || selection == 8)
                                {
                                    Instantiate(cornerList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.identity);
                                } else {
                                    Instantiate(cornerList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, 45, 0));
                                }
                            } else {
                                Instantiate(medObstaclelIst[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                            }
                            break;
                        case 3:
                            selection = Random.Range(2, obstacleList.Length);
                            if(i == (escalation-1) && j == (escalation-1) && i == j)
                            {
                                selection = Random.Range(0, cornerList.Length);
                                if(selection == 0 || selection == 7 || selection == 8)
                                {
                                    Instantiate(cornerList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.identity);
                                } else {
                                    Instantiate(cornerList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, 45, 0));
                                }
                            } else {
                                Instantiate(obstacleList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                            }
                            break;
                        default:
                            selection = Random.Range(2, obstacleList.Length);
                            if(i == (escalation-1) && j == (escalation-1) && i == j)
                            {
                                selection = Random.Range(0, cornerList.Length);
                                if(selection == 0 || selection == 7 || selection == 8)
                                {
                                    Instantiate(cornerList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.identity);
                                } else {
                                    Instantiate(cornerList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, 45, 0));
                                }
                            } else {
                                Instantiate(obstacleList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                            }
                            break;
                    }
                }

                if(escalation == 1)
                {
                    selection = Random.Range(0, easyObstacleList.Length);
                    Instantiate(cornerList[selection], new Vector3(100*i+50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                } else {
                    if(Random.Range(0, 10) != 0)
                    {
                        
                        switch (level)
                        {
                            case 1:
                                selection = Random.Range(0, easyObstacleList.Length);
                                Instantiate(easyObstacleList[selection], new Vector3(100*i+50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                                break;
                            case 2:
                                selection = Random.Range(0, medObstaclelIst.Length);
                                Instantiate(medObstaclelIst[selection], new Vector3(100*i+50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                                break;
                            case 3:
                                selection = Random.Range(2, obstacleList.Length);
                                Instantiate(obstacleList[selection], new Vector3(100*i+50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                                break;
                            default:
                                selection = Random.Range(2, obstacleList.Length);
                                Instantiate(obstacleList[selection], new Vector3(100*i+50, Random.Range(-5, 0), -100*j-50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                                break;
                        }

                    }
                    
                }


                if(escalation == 1)
                {
                    selection = Random.Range(0, easyObstacleList.Length);
                    Instantiate(cornerList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                } else {
                    if(Random.Range(0, 10) != 0)
                    {
                        
                        switch (level)
                        {
                            case 1:
                                selection = Random.Range(0, easyObstacleList.Length);
                                Instantiate(easyObstacleList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                                break;
                            case 2:
                                selection = Random.Range(0, medObstaclelIst.Length);
                                Instantiate(medObstaclelIst[selection], new Vector3(-100*i-50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                                break;
                            case 3:
                                selection = Random.Range(2, obstacleList.Length);
                                Instantiate(obstacleList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                                break;
                            default:
                                selection = Random.Range(2, obstacleList.Length);
                                Instantiate(obstacleList[selection], new Vector3(-100*i-50, Random.Range(-5, 0), 100*j+50),  Quaternion.Euler(0, (45*Random.Range(0,8)), 0));
                                break;
                        }
                    }
                }   

                if(i == escalation-1)
                {
                    Instantiate(boundaries, new Vector3(-100*(i+1), 0, 100*j+50),  Quaternion.identity);
                    Instantiate(boundaries, new Vector3(100*(i+1), 0, -100*j-50),  Quaternion.identity);
                } 

                if (i == escalation-1 && i != j)
                {
                    Instantiate(boundaries, new Vector3(100*(i+1), 0, 100*j+50),  Quaternion.identity);
                    Instantiate(boundaries, new Vector3(-100*(i+1), 0, -100*j-50),  Quaternion.identity);
                }

                if(j == escalation-1)
                {
                    Instantiate(boundaries, new Vector3(-100*i-50, 0, 100*(j+1)),  Quaternion.Euler(0, 90, 0));
                    Instantiate(boundaries, new Vector3(100*i+50, 0, -100*(j+1)),  Quaternion.Euler(0, 90, 0));
                }

                if(j == escalation-1 && i != j)
                {
                    Instantiate(boundaries, new Vector3(100*i+50, 0, 100*(j+1)),  Quaternion.Euler(0, 90, 0));
                    Instantiate(boundaries, new Vector3(-100*i-50, 0, -100*(j+1)),  Quaternion.Euler(0, 90, 0));
                }

    		}

    	}

    	Instantiate(obstacleList[0], new Vector3((100*escalation)+50, 5, (100*escalation)+50),  Quaternion.identity);
    	Instantiate(obstacleList[1], new Vector3(-((100*escalation)+50), -10, -((100*escalation)+50)),  Quaternion.identity);

        script.finished = false;
    }

}
