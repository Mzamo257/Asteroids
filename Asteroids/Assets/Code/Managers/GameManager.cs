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
	private List<SurvivalLevelData> survivalLevelList;
    private List<StoryLevelData> storyLevelList;

	private int currentSurvivalLevel;
    private int currentStoryLevel;
    private GameMode currentGameMode;
    
	#endregion

	#region Properties


	public int CurrentLevel {
		get { return currentSurvivalLevel; }
	}
    // TODO: Manage it with the different level types
	public SurvivalLevelData CurrentSurvivalLevelData
	{
		get{ return survivalLevelList[currentSurvivalLevel]; }
	}
    public StoryLevelData CurrentStoryLevelData
    {
        get { return storyLevelList[currentStoryLevel]; }
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
        // Survival ones
		survivalLevelList = new List<SurvivalLevelData>();
		for(int i = 0; i< 2; i++)
		{
			survivalLevelList.Add(Reader.getSurvivalLevelDataFromXML ("LEVEL1"));
		}
        // Story ones
        storyLevelList = new List<StoryLevelData>();
        for (int i = 0; i < 2; i++)
        {
            storyLevelList.Add(Reader.getStoryLevelDataFromXML("LEVEL1"));
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
	#endregion
}
