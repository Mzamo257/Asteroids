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

public class GameManagerSingleton : MonoBehaviour {

    #region Public Attributes
    public static GameManagerSingleton instance = null;
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

    /*public GameManagerSingleton()
    {
        Start();
        Debug.Log("Starting - " + 
            "Current Survival level: " + currentSurvivalLevel +
                    ", current story level: " + currentStoryLevel);
    }*/

    #region Monobehavoiur Methods

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }
    
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
        Debug.Log("Current level: " + currentSurvivalLevel + ", num levels: " + survivalLevelList.Count);
    }

    public void StoryProgress()
    {
        if (currentStoryLevel < storyLevelList.Count - 1)
        {
            currentStoryLevel++;
        }
        Debug.Log("Current level: " + currentStoryLevel + ", num levels: " + storyLevelList.Count);
    }

    #endregion

}
