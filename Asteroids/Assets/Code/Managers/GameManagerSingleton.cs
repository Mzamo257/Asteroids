using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Invalid = -1,

    Survival,
    Story,
    Competitive,

    //...
    Count,

}

public class GameManagerSingleton : Singleton<GameManagerSingleton> {

    #region Public Attributes

    public GameManagerSingleton instance = null;

    #endregion

    #region Private Attributes
    private string language;
    private bool music;
    private float volume = 1.0f;
    private List<SurvivalLevelData> survivalLevelList;
    private List<StoryLevelData> storyLevelList;

    private int currentSurvivalLevel = 0;
    private int currentStoryLevel = 0;
    private GameMode currentGameMode;

    #endregion

    #region Properties

    public float Volume
    {
        get { return volume; }
		set {
            volume = value;
            PlayerPrefs.SetFloat("Volume", value);
        }
    }
    public int CurrentLevel
    {
        get { return currentSurvivalLevel; }
    }
    // TODO: Manage it with the different level types
    public SurvivalLevelData CurrentSurvivalLevelData
    {
        get { return survivalLevelList[currentSurvivalLevel]; }
    }
    public StoryLevelData CurrentStoryLevelData
    {
        get { return storyLevelList[currentStoryLevel]; }
    }

    public int CurrentSurvivalLevel {
        get { return currentSurvivalLevel; }
        set { currentSurvivalLevel = value; }
    }
    public int CurrentStoryLevel {
        get { return currentStoryLevel; }
        set { currentStoryLevel = value; }
    }

    public bool Music
    {
        get { return music; }
    }

    public GameMode CurrentGameMode
    {
        get { return currentGameMode; }
        set { currentGameMode = value; }
    }

    #endregion

    public GameManagerSingleton()
    {
        Start();
        // Debug.Log("Starting");
    }

    #region Methods
    
    // Not monobeahvoiur
    void Start()
    {
        // Get the player prefs
        language = PlayerPrefs.GetString("Language", "English");
        volume = PlayerPrefs.GetFloat("Volume", 1);
        music = PlayerPrefs.GetInt("MusicOff", 0) == 0;

        //Create the levels
        // Survival ones
        survivalLevelList = Reader.getSurvivalLevelDataFromXML();        
        // Story ones
        storyLevelList = Reader.getStoryLevelDataFromXML();
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
        switch (value)
        {
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
        PlayerPrefs.SetString("Language", language);
    }

    public void SetSound(bool change)
    {
        music = change;
        PlayerPrefs.SetInt("MusicOff", change ? 1 : 0);
    }

    public void SurvivalProgress()
    {
        if(currentSurvivalLevel < survivalLevelList.Count - 1)
        {
            currentSurvivalLevel++;
        }
    }

    public void StoryProgress()
    {
        if (currentStoryLevel < storyLevelList.Count - 1)
        {
            currentStoryLevel++;
        }
    }

    #endregion

}
