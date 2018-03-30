using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TO DO: Cambiar el nombre de la clase
public class BaseLevelManager : MonoBehaviour {

	public GameObject wayPoint_prefab;

	// protected List<GameObject> list_of_wayPoints;
    protected GameManager gameManager;
    protected BaseLevelData level_data;
    protected float spaceshipCurrentLife;

    protected float maxSpaceshipLife = 100;

    #region Properties
    public float SpaceCurrentLife {
		get { return spaceshipCurrentLife; }
	}

	public float SpaceMaxLife {
		get { return maxSpaceshipLife;}
	}
	#endregion

	// Use this for initialization
	protected virtual void Start () 
	{
        gameManager = FindObjectOfType<GameManager>();
		level_data = gameManager.CurrentLevelData;
		spaceshipCurrentLife = maxSpaceshipLife;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

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
}
