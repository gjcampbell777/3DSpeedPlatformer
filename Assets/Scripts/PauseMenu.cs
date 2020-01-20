using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;

    public GameObject pauseMenuUI;
    public GameObject gameOverUI;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI LivesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    	if(SceneManager.GetActiveScene().name == "Floor Level") 
    	{
    		ScoreText.text = "LEVEL: " + FloorLevel.escalation + "-" + FloorLevel.level;
    		LivesText.text = "LIVES: " + PlayerController.shownLives;
    	}

    	if(SceneManager.GetActiveScene().name == "Waypoint Race")
    	{

    		ScoreText.text = "LEVEL: " + (WaypointRace.size - 1) + "-" + WaypointRace.level;
    		TimeText.text = "TIME: " + Mathf.Round((WaypointRace.countdown + WaypointRace.completionTime) - Time.time);

    	}

    	if(Input.GetButtonDown("Pause"))
    	{
    		if(GameIsPaused)
    		{
    			Resume();
    			Cursor.visible = !Cursor.visible;
        		Cursor.lockState = CursorLockMode.Locked;
    		} else {
    			Pause();
    			Cursor.visible = Cursor.visible;
        		Cursor.lockState = CursorLockMode.None;
    		}
    	}

    	if(GameIsOver)
    	{
    		GameOver();
    		Cursor.visible = Cursor.visible;
        	Cursor.lockState = CursorLockMode.None;
    	}

    }

    public void Resume()
    {
    	pauseMenuUI.SetActive(false);
    	Time.timeScale = 1f;
    	GameIsPaused = false;
    	GameIsOver = false;
    	Cursor.visible = !Cursor.visible;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
    	pauseMenuUI.SetActive(true);
    	Time.timeScale = 0f;
    	GameIsPaused = true;
    	Cursor.visible = Cursor.visible;
        Cursor.lockState = CursorLockMode.None;
    }

    void GameOver()
    {
    	if(FloorLevel.gameOver == true)
    	{
    		ScoreText.text = "LEVEL: " + FloorLevel.escalation + "-" + FloorLevel.level;
    		TimeText.text = "TIME: " + Mathf.Round(FloorLevel.fullTime);
    	}

    	if(WaypointRace.gameOver == true)
    	{
    		ScoreText.text = "LEVEL: " + (WaypointRace.size - 1) + "-" + WaypointRace.level;
    		TimeText.text = "TIME: " + Mathf.Round(WaypointRace.fullTime);
    	}

    	gameOverUI.SetActive(true);
    	Time.timeScale = 0f;
    	GameIsPaused = true;
    	Cursor.visible = Cursor.visible;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadHubWorld()
    {
    	Time.timeScale = 1f;
    	GameIsPaused = false;
    	SceneManager.LoadScene("Hub World", LoadSceneMode.Single);
    	Cursor.visible = !Cursor.visible;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMenu()
    {
    	Time.timeScale = 1f;
    	GameIsPaused = false;
    	SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void Quit()
    {

    	Application.Quit();
		Debug.Log("QUIT");

    }
}
