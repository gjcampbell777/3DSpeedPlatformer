using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

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

    void Start()
    {
    	EventSystem.current.SetSelectedGameObject(null);
    	EventSystem.current.SetSelectedGameObject(menuFirstButton);

    	switch (Random.Range(0,5))
    	{
    		case 0:
    			BeanTalk.text = "THAT'S MY GAME!";
    			break;
    		case 1:
    			BeanTalk.text = "GOTTA GO FAST!";
    			break;
    		case 2:
    			BeanTalk.text = "IT ME!";
    			break;
    		case 3:
    			BeanTalk.text = "DO YOUR BEST!";
    			break;
    		case 4:
    			BeanTalk.text = "NEVER GIVE UP!";
    			break;
    		default:
    			BeanTalk.text = "ERROR! SAVE ME!";
    			break;
    	}
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
    	switch (Random.Range(0,5))
    	{
    		case 0:
    			BeanTalk.text = "THAT'S MY GAME!";
    			break;
    		case 1:
    			BeanTalk.text = "GOTTA GO FAST!";
    			break;
    		case 2:
    			BeanTalk.text = "IT ME!";
    			break;
    		case 3:
    			BeanTalk.text = "DO YOUR BEST!";
    			break;
    		case 4:
    			BeanTalk.text = "NEVER GIVE UP!";
    			break;
    		default:
    			BeanTalk.text = "ERROR! SAVE ME!";
    			break;
    	}
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
    	switch (Random.Range(0,5))
    	{
    		case 0:
    			BeanTalk.text = "THAT'S MY GAME!";
    			break;
    		case 1:
    			BeanTalk.text = "GOTTA GO FAST!";
    			break;
    		case 2:
    			BeanTalk.text = "IT ME!";
    			break;
    		case 3:
    			BeanTalk.text = "DO YOUR BEST!";
    			break;
    		case 4:
    			BeanTalk.text = "NEVER GIVE UP!";
    			break;
    		default:
    			BeanTalk.text = "ERROR! SAVE ME!";
    			break;
    	}
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
    	switch (Random.Range(0,5))
    	{
    		case 0:
    			BeanTalk.text = "THAT'S MY GAME!";
    			break;
    		case 1:
    			BeanTalk.text = "GOTTA GO FAST!";
    			break;
    		case 2:
    			BeanTalk.text = "IT ME!";
    			break;
    		case 3:
    			BeanTalk.text = "DO YOUR BEST!";
    			break;
    		case 4:
    			BeanTalk.text = "NEVER GIVE UP!";
    			break;
    		default:
    			BeanTalk.text = "ERROR! SAVE ME!";
    			break;
    	}
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
    	switch (Random.Range(0,5))
    	{
    		case 0:
    			BeanTalk.text = "THAT'S MY GAME!";
    			break;
    		case 1:
    			BeanTalk.text = "GOTTA GO FAST!";
    			break;
    		case 2:
    			BeanTalk.text = "IT ME!";
    			break;
    		case 3:
    			BeanTalk.text = "DO YOUR BEST!";
    			break;
    		case 4:
    			BeanTalk.text = "NEVER GIVE UP!";
    			break;
    		default:
    			BeanTalk.text = "ERROR! SAVE ME!";
    			break;
    	}
	}

	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("QUIT");
	}

}
