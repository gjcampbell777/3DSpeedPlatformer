using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]

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
    public TextMeshProUGUI BeanTalk;
    public string[] phrases;

    void Start()
    {
    	EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(menuFirstButton);
    	BeanTalk.text = phrases[Random.Range(0, phrases.Length)];
    	
    }

	public void PlayGame()
	{
		SceneManager.LoadScene("Hub World", LoadSceneMode.Single);
	}

	public void Skins()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(skinsFirstButton);
    	BeanTalk.text = "WHAT DO I DO AGAIN?";
	}

	public void BackSkins()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(skinsBackButton);
    	BeanTalk.text = phrases[Random.Range(0, phrases.Length)];
	}

	public void Highscores()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(highscoreFirstButton);
    	BeanTalk.text = "GOOD JOB!";
	}

	public void BackHighscores()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(highscoreBackButton);
    	BeanTalk.text = phrases[Random.Range(0, phrases.Length)];
	}

	public void GameInfo()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(gameinfoFirstButton);
    	BeanTalk.text = "SO MANY BUTTONS!";
	}

	public void BackGameInfo()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(gameinfoBackButton);
    	BeanTalk.text = phrases[Random.Range(0, phrases.Length)];
	}

	public void Options()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    	BeanTalk.text = "LOOK AT THOSE GRAPHIX!";
	}

	public void BackOptions()
	{
		EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(optionsBackButton);
    	BeanTalk.text = phrases[Random.Range(0, phrases.Length)];
	}

	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("QUIT");
	}

}
