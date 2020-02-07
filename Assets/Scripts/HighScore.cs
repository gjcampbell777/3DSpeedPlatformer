using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    
    public TextMeshProUGUI ObstacleScoreText;
    public TextMeshProUGUI ObstacleTimeText;
    public TextMeshProUGUI WaypointScoreText;
    public TextMeshProUGUI WaypointTimeText;
    public TextMeshProUGUI InfinityScoreText;
    public TextMeshProUGUI InfinityTimeText;

    // Update is called once per frame
    void Update()
    {
        
    	ObstacleScoreText.text = "HIGH SCORE: " + "\n" + PlayerPrefs.GetInt("ObstacleHighFloor")
			+ "-" + PlayerPrefs.GetInt("ObstacleHighLevel");
		ObstacleTimeText.text = "HIGH SCORE TIME: " + "\n" + Mathf.Round(PlayerPrefs.GetFloat("ObstacleHighTime")); 

		WaypointScoreText.text = "HIGH SCORE: " + "\n" + PlayerPrefs.GetInt("WaypointHighSize")
			+ "-" + PlayerPrefs.GetInt("WaypointHighFloor");
		WaypointTimeText.text = "HIGH SCORE TIME: " + "\n" + Mathf.Round(PlayerPrefs.GetFloat("WaypointHighTime")); 

		InfinityScoreText.text = "HIGH SCORE: " + "\n" + PlayerPrefs.GetInt("RunnerHighFloor")
			+ "-" + PlayerPrefs.GetInt("RunnerHighLevel");
		InfinityTimeText.text = "HIGH SCORE TIME: " + "\n" + Mathf.Round(PlayerPrefs.GetFloat("RunnerHighTime")); 

    }
}
