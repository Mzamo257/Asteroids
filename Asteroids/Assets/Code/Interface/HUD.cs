using UnityEngine;

public abstract class HUD : MonoBehaviour {
	#region Public Attributes
	public Texture[] alertBars;
	public float iconSizeRate = 0.2f;
	public float damage;
	public Canvas Lose;
	public Canvas Win;
	public Canvas Pause;
	public GUIStyle levelStyle;
	public Texture back;
	public Texture panel;
    public Texture asteroidPointer;
    #endregion

    #region Private Attributes
    protected float iconSize;
	protected GameObject gameManager;
    protected GameManagerSingleton gmSingScript;
	protected BaseLevelManager levelMgr;
    protected string finalScoreText;
    protected string scoreText;
    protected string levelText;
	#endregion


	#region MonoDevelop Methods
	// Use this for initialization
	protected virtual void Start () {

        levelMgr = FindObjectOfType<BaseLevelManager> ();

		iconSize = Screen.height * iconSizeRate;

		damage = levelMgr.SpaceCurrentLife;

        gmSingScript = FindObjectOfType<GameManagerSingleton>();

        levelStyle.fontSize = (int)(Screen.height * 0.03f);

        Language();
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (damage > levelMgr.SpaceCurrentLife)	damage -= 10 * Time.deltaTime;	
	}

	protected virtual void OnGUI()
	{
		if (levelMgr.currentState == GameState.Victory) {
			Win.enabled = true;
			Pause.enabled = false;
            //Points panel
			GUI.DrawTexture (new Rect (Screen.width * 2.9f / 10, Screen.height *4.5f / 10, Screen.width * 4.3f / 10, Screen.height *2/ 10), panel, ScaleMode.StretchToFill);
            //Score texts
            GUI.TextField(new Rect(Screen.width * 3.5f/10, Screen.height * 5/10, Screen.width * 4/10, Screen.height * 0.5f/10), scoreText + ": " + levelMgr.Score, levelStyle);
            GUI.TextField(new Rect(Screen.width * 3.5f/10, Screen.height * 5.8f/10, Screen.width * 4/10, Screen.height * 0.5f/10), finalScoreText + ": " + gmSingScript.CurrentScore, levelStyle);

        } else if (levelMgr.currentState == GameState.Defeat) {
			Lose.enabled = true;
			Pause.enabled = false;
            //Points panel
			GUI.DrawTexture (new Rect (Screen.width * 2.9f / 10, Screen.height *5.5f / 10, Screen.width * 4.3f / 10, Screen.height *1/ 10), panel, ScaleMode.StretchToFill);
            //Score texts
			GUI.TextField(new Rect(Screen.width * 3.5f/10, Screen.height * 5.8f/10, Screen.width * 4/10, Screen.height * 0.5f/10), finalScoreText + ": " + gmSingScript.CurrentScore, levelStyle);

        } else if (levelMgr.currentState != GameState.Paused) {
			Pause.enabled = true;
			//Healthbar
			GUI.DrawTexture (new Rect (Screen.width*2f/10, Screen.height*0.6f/10, damage / levelMgr.SpaceMaxLife * (Screen.width*4f/10), Screen.height*0.75f/10), alertBars [2], ScaleMode.StretchToFill);
			GUI.DrawTexture (new Rect (Screen.width*2f/10, Screen.height*0.6f/10, levelMgr.SpaceCurrentLife / levelMgr.SpaceMaxLife * (Screen.width*4/10),Screen.height*0.75f/10), alertBars [1], ScaleMode.StretchToFill);
			GUI.DrawTexture (new Rect (Screen.width*2f/10, Screen.height*0.6f/10, Screen.width*4/10, Screen.height*0.75f/10), alertBars [0], ScaleMode.StretchToFill);
            //Asteroid pointer
			if (levelMgr.AsteroidSelected) {
				GUI.DrawTexture (new Rect (levelMgr.AsteroidPosCamera.x - asteroidPointer.width * 0.04f, levelMgr.AsteroidPosCamera.y - asteroidPointer.height * 0.02f, iconSize / 1.5f, iconSize / 1.5f), asteroidPointer, ScaleMode.ScaleToFit);
			}
		}
	}
	#endregion

	#region User Methods
    void Language()
    {
        switch (gmSingScript.GetLanguage())
        {
            case "English":
                scoreText = "Your Score";
                finalScoreText = "Total Score";
                levelText = "Level";
                break;
            case "Español":
                scoreText = "Puntuacion";
                finalScoreText = "Puntuacion total";
                levelText = "Nivel";
                break;
            case "Français":
                scoreText = "Final score";
                finalScoreText = "Menu Principal";
                levelText = "Level";
                break;
        }
    }
	#endregion
}
