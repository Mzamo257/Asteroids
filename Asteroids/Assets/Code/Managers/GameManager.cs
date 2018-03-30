using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Public Attributes
    public enum GameMode
    {
        Invalid = -1,

        Survival,
        Story,
        Competitive,

        Count
    }
    #endregion

    #region Private Attributes
    private string language;
	private bool music;
	private List<data_Level> levels_List;

	private int current_Level;
    //private float damage = 0;
    private GameMode gameMode;
    
	#endregion

	#region Properties


	public int Current_Level {
		get { return current_Level; }
	}

	public data_Level current_Level_data
	{
		get{ return levels_List[current_Level]; }
	}

	public bool mute {
		get{ return music; }
	}
		

	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start () {
		language = "English";
		SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
		//Create the levels
		levels_List = new List<data_Level>();
		for(int i = 0; i< 2; i++)
		{
			levels_List.Add(Reader.getDataFromXML ("LEVEL1"));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#endregion

	#region User Methods

	public string GetLanguage()
	{
		return language;
	}

	public bool GetSound()
	{
		return music;
	}

    public void DamageSpaceShip( float damage)
    {
		spaceshipCurrentLife -= damage;
		spaceshipCurrentLife = Mathf.Max(0, spaceshipCurrentLife);
		if(spaceshipCurrentLife == 0)
        {
            // Aquí el gameover/victoria
            switch (gameMode)
            {
                case GameMode.Survival:
                    Debug.Log("Victory");
                    break;
                case GameMode.Story:
                    Debug.Log("Defeat");
                    break;
                case GameMode.Competitive:
                    Debug.Log("Player Asteroid wins");
                    break;
            }
            SceneManager.LoadScene("Menu");
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
	#endregion
}
