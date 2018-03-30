using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// List of the game modes
/// </summary>
public enum GameMode
{
    Invalid = -1,

    Survival,
    Story,
    Competitive,

    Count
}

/// <summary>
/// Game manager class
/// </summary>
public class GameManager : MonoBehaviour {

    #region Public Attributes
    
    #endregion

    #region Private Attributes
    private string language;
	private bool music;
	private List<BaseLevelData> levels_List;

	private int currentLevel;
    //private float damage = 0;
    private GameMode currentGameMode;
    
	#endregion

	#region Properties


	public int CurrentLevel {
		get { return currentLevel; }
	}

	public BaseLevelData CurrentLevelData
	{
		get{ return levels_List[currentLevel]; }
	}

	public bool mute {
		get{ return music; }
	}
		
    public GameMode CurrentGameMode
    {
        get { return currentGameMode; }
        set { currentGameMode = value; }
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
		levels_List = new List<BaseLevelData>();
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
