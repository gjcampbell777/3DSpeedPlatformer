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

    // Update is called once per frame
    void Update()
    {

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
    	EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(gameOverFirstButton);

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

    public void Quit()
    {

    	Application.Quit();
		Debug.Log("QUIT");

    }
}
