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

    public GameObject pauseFirstButton;
    public GameObject gameOverFirstButton;

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
    	}

    	if(WaypointRace.gameOver == true)
    	{
    		ScoreText.text = "LEVEL: " + (WaypointRace.size - 1) + "-" + WaypointRace.level;
    		TimeText.text = "TIME: " + Mathf.Round(WaypointRace.fullTime);
    	}

    	if(InfinityRunner.gameOver == true)
    	{
    		ScoreText.text = "LEVEL: " + InfinityRunner.escalation + "-" + InfinityRunner.level;
    		TimeText.text = "TIME: " + Mathf.Round(InfinityRunner.fullTime);
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
    		SceneManager.LoadScene("Floor Level", LoadSceneMode.Single);
    	}

    	if(WaypointRace.gameOver == true)
    	{
    		SceneManager.LoadScene("Waypoint Race", LoadSceneMode.Single);
    	}

    	if(InfinityRunner.gameOver == true)
    	{
    		SceneManager.LoadScene("Infinity Runner", LoadSceneMode.Single);
    	}

    }

    public void Quit()
    {

    	Application.Quit();
		Debug.Log("QUIT");

    }
}
