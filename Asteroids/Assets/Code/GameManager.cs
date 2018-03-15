using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private string language;
	private bool music;
	private float spaceshipLife = 100;

    private float spaceshipCurrentLife;
    //private float damage = 0;

    public float SpaceCurrentLife {
        get { return spaceshipCurrentLife; }
    }

    // Use this for initialization
    void Awake()
	{
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start () {
		language = "English";
		SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
        spaceshipCurrentLife = spaceshipLife;
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

    public void DamageSpaceShip( float damage)
    {
        spaceshipLife -= damage;
        spaceshipLife = Mathf.Max(0, spaceshipLife);
        if(spaceshipLife == 0)
        {
            // Aquí el gameover
        }
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

	/*public float GetDamage()
	{
		return damage;
	}*/
}
