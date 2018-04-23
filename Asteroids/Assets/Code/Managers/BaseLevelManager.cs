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
    #endregion

    #region Private Attributes
    // protected List<GameObject> list_of_wayPoints;
    //protected GameManager gameManager;
    protected GameManagerSingleton gameManagerSingleton;
    // For the camera check
    protected Transform cameraTransform;
    //protected BaseLevelData level_data; // Change format
    protected float spaceshipCurrentLife;

    protected float maxSpaceshipLife = 300;
    // TODO: Revisar cuando esté bien hecha la herencia de la nave
    protected BaseSpaceship ship;

	protected bool asteroidSelected;
	protected Vector2 asteroidPosCamera;
    //
	protected bool pause;
	protected bool win = false;
	protected bool lose = false;
    protected int manualCounter;
    protected GameState gameState = GameState.WaitingToStart;
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

	public bool Pause {
		get { return pause; }
	}
	public bool Win {
		get { return win; }
	}
	public bool Lose {
		get { return lose; }
	}

	public GameState currentState {
		get { return gameState; }
	}
    #endregion

    #region MonoBahaviour Methods

    // Use this for initialization
    protected virtual void Start () 
	{
        //gameManager = FindObjectOfType<GameManager>();
        gameManagerSingleton = GameManagerSingleton.GetInstance();
        // TODO: Hacer esto en los hijos, cada uno con su estilo de nivel
		// level_data = gameManager.CurrentLevelData;
		spaceshipCurrentLife = maxSpaceshipLife;
        ship = FindObjectOfType<BaseSpaceship>();
        //
        cameraTransform = Camera.main.transform;
        // Start stuff
        Time.timeScale = 0.0f;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        if(gameState == GameState.WaitingToStart)
        {
            // Contando 3 segundos manualmente
            manualCounter++;
			if(introEnd ())
            {
                Time.timeScale = 1.0f;
                gameState = GameState.InGame;
            }
        }
        CheckAsteroidsBetweenCameraAndShip();
    }

    #endregion

    #region Methods

    /// <summary>
    /// 
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
				    Debug.Log ("Victory");
                    gameState = GameState.Victory;
                    Time.timeScale = 0.0f;
                    break;
			    case GameMode.Story:
				    Debug.Log ("Defeat");
                    gameState = GameState.Defeat;
                    Time.timeScale = 0.0f;
                    break;
                case GameMode.Competitive:
                    Debug.Log("Player Asteroid wins");
                    break;
            }
			// PauseGame ();
        }
    }

	//TODO: Cambiar valores por el tamaño de los asteroides
	public void asteroidPosition(Vector3 aPos)
	{
		Vector3 camPoint = Camera.main.WorldToViewportPoint (aPos);
		asteroidPosCamera = new Vector2 (camPoint.x * Screen.width - 10, Screen.height - (camPoint.y * Screen.height) - 20);
	}

    public void PauseGame()
    {
		pause = true;
        gameState = GameState.Paused;
        Time.timeScale = 0.0f;
    }

    public void UnPauseGame()
    {
		pause = false;
        gameState = GameState.InGame;
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// 
    /// </summary>
    void CheckAsteroidsBetweenCameraAndShip()
    {
        RaycastHit hitInfo;
        Vector3 shipDirection = ship.transform.position - cameraTransform.position;
        // Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red);
        //Debug.Log
        if(Physics.Raycast(cameraTransform.position, shipDirection, out hitInfo, shipDirection.magnitude))
        {
            // Debug.Log("Ray hit: " + hitInfo.transform.tag);
            // Metodo 1
            AsteroidCollisionManager acm = hitInfo.transform.GetComponent<AsteroidCollisionManager>();
            if (acm != null)
            {
                // Debug.Log("Asteroid in the path");
                acm.gameObject.SetActive(false);
            }
        }
    }

	public virtual bool introEnd (){return false;}

    #endregion
}
