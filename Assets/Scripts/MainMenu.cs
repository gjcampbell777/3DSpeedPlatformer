using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    
	public GameObject menuFirstButton;
    public GameObject skinsFirstButton;
    public GameObject highscoreFirstButton;
    public GameObject gameinfoFirstButton;
    public GameObject optionsFirstButton;
    public GameObject skinsBackButton;
    public GameObject highscoreBackButton;
    public GameObject gameinfoBackButton;
    public GameObject optionsBackButton;

    void Start()
    {
    	EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(menuFirstButton);
    }

	public void PlayGame()
	{
		SceneManager.LoadScene("Hub World", LoadSceneMode.Single);
	}

	public void Skins()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(skinsFirstButton);
	}

	public void BackSkins()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(skinsBackButton);
	}

	public void Highscores()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(highscoreFirstButton);
	}

	public void BackHighscores()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(highscoreBackButton);
	}

	public void GameInfo()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(gameinfoFirstButton);
	}

	public void BackGameInfo()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(gameinfoBackButton);
	}

	public void Options()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(optionsFirstButton);
	}

	public void BackOptions()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(optionsBackButton);
	}

	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("QUIT");
	}

}
