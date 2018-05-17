using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonSurvival()
	{
		SceneManager.LoadScene ("Survival");
        GameManagerSingleton.instance.CurrentSurvivalLevel = 0;
    }

	public void ButtonStory()
	{
		SceneManager.LoadScene ("Story");
        GameManagerSingleton.instance.CurrentStoryLevel = 0;
    }

	public void ButtonMainMenu()
	{
		SceneManager.LoadScene ("Menu");
		//Time.timeScale = 1.0f;
	}

	public void ButtonResume()
	{
		//GameObject.Find ("Player").GetComponent<PlayerControl>().UnsetPauseStatus();
	}

	public void ButtonExit()
	{
		Application.Quit();
	}

    public void ButtonNextLevel()
    {
        switch (GameManagerSingleton.instance.CurrentGameMode)
        {
            case GameMode.Survival:
                SceneManager.LoadScene("Survival");
                break;
            case GameMode.Story:
                SceneManager.LoadScene("Story");
                break;
        }
        //Time.timeScale = 1;
    }
}
