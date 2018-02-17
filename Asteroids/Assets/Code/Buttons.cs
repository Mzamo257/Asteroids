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
	}

	public void ButtonHistory()
	{
		SceneManager.LoadScene ("History");
	}

	public void ButtonMainMenu()
	{
		SceneManager.LoadScene ("Menu");
		Time.timeScale = 1.0f;
	}

	public void ButtonResume()
	{
		//GameObject.Find ("Player").GetComponent<PlayerControl>().UnsetPauseStatus();
	}

	public void ButtonExit()
	{
		Application.Quit();
	}
}
