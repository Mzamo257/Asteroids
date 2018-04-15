using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TO DO: Cambiar el nombre de la clase
public class BaseLevelManager : MonoBehaviour {

    #region Public Attributes
    public GameObject wayPoint_prefab;
    #endregion

    #region Private Attributes
    // protected List<GameObject> list_of_wayPoints;
    protected GameManager gameManager;
    //protected BaseLevelData level_data; // Change format
    protected float spaceshipCurrentLife;

    protected float maxSpaceshipLife = 300;
    // TODO: Revisar cuando esté bien hecha la herencia de la nave
    protected BaseSpaceship ship;
    #endregion

    #region Properties
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
    #endregion

    #region MonoBahaviour Methods
    // Use this for initialization
    protected virtual void Start () 
	{
        gameManager = FindObjectOfType<GameManager>();
        // TODO: Hacer esto en los hijos, cada uno con su estilo de nivel
		// level_data = gameManager.CurrentLevelData;
		spaceshipCurrentLife = maxSpaceshipLife;
        ship = FindObjectOfType<BaseSpaceship>();
    }
	
	// Update is called once per frame
	protected virtual void Update () {
		
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
            switch (gameManager.CurrentGameMode)
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

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
    }

    #endregion
}
