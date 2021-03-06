using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Invalid = -1,

    WaitingToStart,
    InGame,
    Paused,
    Victory,
    Defeat

}

//TO DO: Cambiar el nombre de la clase
public class BaseLevelManager : MonoBehaviour {

    #region Public Attributes

    public GameObject wayPoint_prefab;
	public GUIStyle textStyle;
    public GameObject explosion;
    public float maxSpaceshipLife = 300;
	public GUIStyle numberStyle;

    #endregion

    #region Private Attributes

    protected GameManagerSingleton gameManagerSingleton;
    protected AsteroidManager asteroidManager;
    // For the camera check
    protected Transform cameraTransform;
    
    protected float spaceshipCurrentLife;

    
    // 
    protected BaseSpaceship ship;

	protected bool asteroidSelected;
	protected Vector2 asteroidPosCamera;
    //
    protected int manualCounter;
    protected GameState gameState = GameState.WaitingToStart;
	protected int startedAsteroids = 0;
	protected bool inIntro;
    //
    protected int score;
    #endregion

    #region Properties

    public int ManualCounterForHUD { get { return manualCounter; } }

    public float SpaceCurrentLife {
		get { return spaceshipCurrentLife; }
        set
        {
            spaceshipCurrentLife = value;
            spaceshipCurrentLife = Mathf.Clamp(spaceshipCurrentLife, 0, maxSpaceshipLife);
        }
	}

	public int StartedAsteroids {
		get { return startedAsteroids; }
		set
		{
			startedAsteroids = value;
		}
	}

	public float SpaceMaxLife {
		get { return maxSpaceshipLife;}
	}

	public Vector2 AsteroidPosCamera {
		get { return asteroidPosCamera; }
	}

	public bool AsteroidSelected {
		get { return asteroidSelected; }
		set { asteroidSelected = value; }
	}

	public bool InIntro {
		get { return inIntro; }
		set { inIntro = value; }
	}

	public GameState currentState {
		get { return gameState; }
		set { gameState = value; }
	}

    public int Score
    {
        get { return score; }
        //set { score = value; }
    }

    #endregion

    #region MonoBahaviour Methods

    // Use this for initialization
    protected virtual void Start () 
	{
        gameManagerSingleton = GameManagerSingleton.instance;
        asteroidManager = FindObjectOfType<AsteroidManager>();

		spaceshipCurrentLife = maxSpaceshipLife;
        ship = FindObjectOfType<BaseSpaceship>();
        //
        cameraTransform = Camera.main.transform;
        // Start stuff
		inIntro = true;
		currentState = GameState.WaitingToStart;
        Time.timeScale = 0.0f;
		textStyle.fontSize = (int)(Screen.height * 0.04f);
		numberStyle.fontSize = (int)(Screen.height * 0.8f);
    }
	
	// Update is called once per frame
	protected virtual void Update () {
		if(gameState == GameState.WaitingToStart && !inIntro)
        {
            // Counting 3 seconds manually
            manualCounter++;
			if(manualCounter > 180)
            {
                Time.timeScale = 1.0f;
                gameState = GameState.InGame;
            }
        }
		else
        	CheckAsteroidsBetweenCameraAndShip();
    }

	void OnGUI()
	{
		if(gameState == GameState.WaitingToStart && !inIntro)
		{
			if (manualCounter < 60)
				GUI.Label (new Rect (Screen.width * 4 / 10, Screen.height * 2 / 10, Screen.width * 2 / 10, Screen.height * 2f / 10), "3", numberStyle);
			else if (manualCounter < 120)
				GUI.Label (new Rect (Screen.width * 4 / 10, Screen.height * 2 / 10, Screen.width * 2 / 10, Screen.height * 2f / 10), "2", numberStyle);
			else 
				GUI.Label (new Rect (Screen.width * 4 / 10, Screen.height * 2 / 10, Screen.width * 2 / 10, Screen.height * 2f / 10), "1", numberStyle);
		}
	}
    #endregion

    #region Methods

    /// <summary>
    /// Apply the damage done by the hit to the spaceship
    /// And decide what to do with it if the ship health reaches 0
    /// Victory in survival, defeat in story
    /// </summary>
    /// <param name="damage"></param>
    public void DamageSpaceShip(float damage)
    {
        spaceshipCurrentLife -= damage;
        spaceshipCurrentLife = Mathf.Max(0, spaceshipCurrentLife);
        if (spaceshipCurrentLife == 0)
        {
            // Aquí el gameover/victoria
            switch(gameManagerSingleton.CurrentGameMode)
            {
			    case GameMode.Survival:
                    gameState = GameState.Victory;
                    score = GetScoreFromWaypoints();
                    gameManagerSingleton.CurrentScore += score;
                    GameManagerSingleton.instance.SurvivalProgress();
                    break;
			    case GameMode.Story:
				    Debug.Log ("Defeat");
                    gameState = GameState.Defeat;
                    break;
                case GameMode.Competitive:
                    Debug.Log("Player Asteroid wins");
                    break;
            }
            explosion.SetActive(true);
            ship.BeDestroyed();
            Effects effectsManager = FindObjectOfType<Effects>();
            effectsManager.playEffect(1);
        }
    }

    /// <summary>
    /// Convert position of the asteroid in the world into camera coordinates
    /// </summary>
    /// <param name="aPos"> Asteroid Position </param>
	public void asteroidPosition(Vector3 aPos)
	{
		Vector3 camPoint = Camera.main.WorldToViewportPoint (aPos);
		asteroidPosCamera = new Vector2 (camPoint.x * Screen.width /*- 10*/, Screen.height - (camPoint.y * Screen.height)/* - 20*/);
	}

    public void PauseGame()
    {
        gameState = GameState.Paused;
        Time.timeScale = 0.0f;
    }

    public void UnPauseGame()
    {
        gameState = GameState.InGame;
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Make dissapear the asteroids that obstruct the view of the ship
    /// </summary>
    void CheckAsteroidsBetweenCameraAndShip()
    {
        RaycastHit hitInfo;
        Vector3 shipDirection = ship.transform.position - cameraTransform.position;
        if(Physics.Raycast(cameraTransform.position, shipDirection, out hitInfo, shipDirection.magnitude))
        {
            AsteroidCollisionManager acm = hitInfo.transform.GetComponent<AsteroidCollisionManager>();
            if (acm != null)
            {
                acm.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Give the score depending the remaining waypoints/hooks
    /// </summary>
    protected virtual int GetScoreFromWaypoints()
    {
        // Functioning in survival child
        return 0;
    }
    #endregion
}
