using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;

    public GameObject pauseMenuUI;
    public GameObject gameOverUI;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI MiddleText;

    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI HighTimeText;
    public TextMeshProUGUI CongratsText;

    public GameObject pauseFirstButton;
    public GameObject gameOverFirstButton;

    private bool highscore = false;
    private int gameOver = 0;

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

    	if(SceneManager.GetActiveScene().name == "Infinity Runner") 
    	{
    		ScoreText.text = "LEVEL: " + InfinityRunner.escalation + "-" + InfinityRunner.level;
    		LivesText.text = "LIVES: " + PlayerController.shownLives;
    		MiddleText.text = "DISTANCE: " + Mathf.Round(InfinityRunner.distance);
    	}

    	if(Input.GetButtonDown("Pause"))
    	{
    		if(GameIsPaused)
    		{
    			Resume();

    		} else {

    			Pause();

    		}
    	}

    	if(GameIsOver)
    	{
    		GameOver();

    		if(gameOver == 0)
    		{
    			EventSystem.current.SetSelectedGameObject(null);
    			EventSystem.current.SetSelectedGameObject(gameOverFirstButton);
    		}

    		gameOver++;

    	}

    }

    public void Resume()
    {
    	Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    	pauseMenuUI.SetActive(false);
    	gameOverUI.SetActive(false);
    	Time.timeScale = 1f;
    	GameIsPaused = false;
    	GameIsOver = false;
    }

    void Pause()
    {
    	Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    	pauseMenuUI.SetActive(true);
    	Time.timeScale = 0f;
    	GameIsPaused = true;
    	EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    void GameOver()
    {

    	Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverUI.SetActive(true);
    	Time.timeScale = 0f;
    	GameIsPaused = true;

    	if(FloorLevel.gameOver == true)
    	{
    		ScoreText.text = "LEVEL: " + FloorLevel.escalation + "-" + FloorLevel.level;
    		TimeText.text = "TIME: " + Mathf.Round(FloorLevel.fullTime);

    		if(FloorLevel.escalation > PlayerPrefs.GetInt("ObstacleHighFloor") || 
    			(FloorLevel.escalation == PlayerPrefs.GetInt("ObstacleHighFloor") &&
    			FloorLevel.level >= PlayerPrefs.GetInt("ObstacleHighLevel")))
    		{

    			if(FloorLevel.escalation > PlayerPrefs.GetInt("ObstacleHighFloor") || 
    			FloorLevel.level > PlayerPrefs.GetInt("ObstacleHighLevel") ||
    			(FloorLevel.level == PlayerPrefs.GetInt("ObstacleHighLevel") &&
    			Mathf.Round(FloorLevel.fullTime) < PlayerPrefs.GetFloat("ObstacleHighTime")))
    			{
    				PlayerPrefs.SetInt("ObstacleHighFloor", FloorLevel.escalation);
    				PlayerPrefs.SetInt("ObstacleHighLevel", FloorLevel.level);
    				PlayerPrefs.SetFloat("ObstacleHighTime", Mathf.Round(FloorLevel.fullTime));

    				highscore = true;
    			}

    		}

    		if(highscore) 
    		{
    			CongratsText.text = "NEW" + "\n" + "HIGH SCORE!" + "\n" + "CONGRATS!";
    		} else {
    			CongratsText.text = "TRY AGAIN!";
    		}

    		HighScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("ObstacleHighFloor")
    			+ "-" + PlayerPrefs.GetInt("ObstacleHighLevel");
    		HighTimeText.text = "HIGH SCORE TIME: " + Mathf.Round(PlayerPrefs.GetFloat("ObstacleHighTime")); 
    	}

    	if(WaypointRace.gameOver == true)
    	{
    		ScoreText.text = "LEVEL: " + (WaypointRace.size - 1) + "-" + WaypointRace.level;
    		TimeText.text = "TIME: " + Mathf.Round(WaypointRace.fullTime);

    		if((WaypointRace.size - 1) > PlayerPrefs.GetInt("WaypointHighSize") || 
    			((WaypointRace.size - 1) == PlayerPrefs.GetInt("WaypointHighSize") &&
    			WaypointRace.level >= PlayerPrefs.GetInt("WaypointHighFloor")))
    		{

    			if((WaypointRace.size - 1) > PlayerPrefs.GetInt("WaypointHighSize") || 
    			WaypointRace.level > PlayerPrefs.GetInt("WaypointHighFloor") ||
    			(WaypointRace.level == PlayerPrefs.GetInt("WaypointHighFloor") &&
    			Mathf.Round(WaypointRace.fullTime) < PlayerPrefs.GetFloat("WaypointHighTime")))
    			{
    				PlayerPrefs.SetInt("WaypointHighSize", (WaypointRace.size - 1));
    				PlayerPrefs.SetInt("WaypointHighFloor", WaypointRace.level);
    				PlayerPrefs.SetFloat("WaypointHighTime", Mathf.Round(WaypointRace.fullTime));

    				highscore = true;
    			}

    		}

    		if(highscore) 
    		{
    			CongratsText.text = "NEW" + "\n" + "HIGH SCORE!" + "\n" + "CONGRATS!";
    		} else {
    			CongratsText.text = "TRY AGAIN!";
    		}

    		HighScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("WaypointHighSize")
    			+ "-" + PlayerPrefs.GetInt("WaypointHighFloor");
    		HighTimeText.text = "HIGH SCORE TIME: " + Mathf.Round(PlayerPrefs.GetFloat("WaypointHighTime")); 

    	}

    	if(InfinityRunner.gameOver == true)
    	{
    		ScoreText.text = "LEVEL: " + InfinityRunner.escalation + "-" + InfinityRunner.level;
    		TimeText.text = "DISTANCE: " + Mathf.Round(InfinityRunner.distance);

    		if(InfinityRunner.escalation > PlayerPrefs.GetInt("RunnerHighFloor") || 
    			(InfinityRunner.escalation == PlayerPrefs.GetInt("RunnerHighFloor") &&
    			InfinityRunner.level >= PlayerPrefs.GetInt("RunnerHighLevel")))
    		{

    			if(InfinityRunner.escalation > PlayerPrefs.GetInt("RunnerHighFloor") || 
    			InfinityRunner.level > PlayerPrefs.GetInt("RunnerHighLevel") ||
    			(InfinityRunner.level == PlayerPrefs.GetInt("RunnerHighLevel") &&
    			Mathf.Round(InfinityRunner.distance) > PlayerPrefs.GetFloat("RunnerHighDistance")))
    			{
    				PlayerPrefs.SetInt("RunnerHighFloor", InfinityRunner.escalation);
    				PlayerPrefs.SetInt("RunnerHighLevel", InfinityRunner.level);
    				PlayerPrefs.SetFloat("RunnerHighDistance", Mathf.Round(InfinityRunner.distance));

    				highscore = true;
    			}

    		}

    		if(highscore) 
    		{
    			CongratsText.text = "NEW" + "\n" + "HIGH SCORE!" + "\n" + "CONGRATS!";
    		} else {
    			CongratsText.text = "TRY AGAIN!";
    		}

    		HighScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("RunnerHighFloor")
    			+ "-" + PlayerPrefs.GetInt("RunnerHighLevel");
    		HighTimeText.text = "HIGH SCORE DISTANCE: " + Mathf.Round(PlayerPrefs.GetFloat("RunnerHighDistance")); 
    	}

    }

    public void LoadHubWorld()
    {
    	Time.timeScale = 1f;
    	GameIsPaused = false;
    	SceneManager.LoadScene("Hub World", LoadSceneMode.Single);
    	Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMenu()
    {
    	Time.timeScale = 1f;
    	GameIsPaused = false;
    	SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    	Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Retry()
    {
    	Time.timeScale = 1f;
    	GameIsPaused = false;
    	Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if(FloorLevel.gameOver == true)
    	{
    		FloorLevel.gameOver = false;
    		SceneManager.LoadScene("Floor Level", LoadSceneMode.Single);
    	}

    	if(WaypointRace.gameOver == true)
    	{
    		WaypointRace.gameOver = false;
    		SceneManager.LoadScene("Waypoint Race", LoadSceneMode.Single);
    	}

    	if(InfinityRunner.gameOver == true)
    	{
    		InfinityRunner.gameOver = false;
    		SceneManager.LoadScene("Infinity Runner", LoadSceneMode.Single);
    	}

    }

    public void Quit()
    {

    	Application.Quit();
		Debug.Log("QUIT");

    }
}
