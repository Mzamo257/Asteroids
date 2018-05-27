using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

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
	}

	public void ButtonExit()
	{
		Application.Quit();
	}

    public void ButtonNextLevel()
    {
        if (GameManagerSingleton.instance.IsFinalLevel)
        {
            SceneManager.LoadScene("Victory");
            return;
        }

        switch (GameManagerSingleton.instance.CurrentGameMode)
        {
            case GameMode.Survival:
                   SceneManager.LoadScene("Survival");
                break;
            case GameMode.Story:
                SceneManager.LoadScene("Story");
                break;
        }

    }
}
