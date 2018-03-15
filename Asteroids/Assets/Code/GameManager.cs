using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private string language;
	private bool music;
	private float spaceshipLife = 100;
	private float damage = 80;

	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start () {
		language = "English";
		SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string GetLanguage()
	{
		return language;
	}

	public bool GetSound()
	{
		return music;
	}

	public float GetSpaceshipLife()
	{
		return spaceshipLife;
	}

	public void ChangeLanguage(int value)
	{
		switch (value) {
		case 0:
			language = "English";
			break;
		case 1:
			language = "Español";
			break;
		case 2:
			language = "Français";
			break;
		}
	}

	public void SetSound(bool change)
	{
		music = change;
	}

	public float GetDamage()
	{
		return damage;
	}
}
