using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    
	public GameObject menuFirstButton;
    public GameObject skinsFirstButton;
    public GameObject optionsFirstButton;
    public GameObject skinsBackButton;
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
