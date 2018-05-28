using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour 
{
    #region Public Attributes
    public List<GameObject> asteroidsPrefabs; //Types of asteoids
    public int numAsteroids; // Number of asteroids of each typr
    public GameObject asteroidLineRendererPrefab;
    #endregion

    #region Private Attributes
    private List<GameObject>[] asteroids; //Asteroids in game
    private GameObject spaceShip;
    private float counter;
    private BaseLevelManager level;
    private LineRenderer asteroidLineRenderer;
    private int maxAsteroids;
    private int currentActiveAsteroids;
    #endregion

    #region Properties
    public LineRenderer AsteroidLineRenderer 
	{ 
		get 
		{ 
			return asteroidLineRenderer; 
		} 
	}

    public int CurrentActiveAsteroids 
	{
        set 
		{ 
			currentActiveAsteroids = value; 
		}
        get 
		{ 
			return currentActiveAsteroids; 
		}
    }
    #endregion

    #region MonoDevelop Methods
    void Start () 
	{
		spaceShip = GameObject.Find("Viper");
        establishAsteoidPools();
		level = FindObjectOfType<BaseLevelManager>();
        //
        GameObject instantiatedAsteroidLineRenderer = Instantiate(asteroidLineRendererPrefab, Vector3.zero, Quaternion.identity);
        asteroidLineRenderer = instantiatedAsteroidLineRenderer.GetComponent<LineRenderer>();
        instantiatedAsteroidLineRenderer.SetActive(false);
        maxAsteroids = asteroids.Length * asteroids[0].Count;
	}

	// Update is called once per frame
	void Update () 
	{
		counter += Time.deltaTime;

		if (counter >= 0.3) 
		{
			CheckAsteroids();
			counter = 0;
		}

		//if the intro is finished, they asdigned a 4 to StartedAsteroids, and activate the procedural spam of asteoids.
		if (level.StartedAsteroids == 4) 
		{
            // Determine the proportion of active asteroids depending in the number of asteroid activated at that moment
            float activeAsteroidProportion = (float)currentActiveAsteroids / (float)maxAsteroids;

            int maxAsteroidsToSpawn = 25;
            int minAsteroidsToSpawn = 1;
            int asteroidsToSpawn = (int)((1 - activeAsteroidProportion) * (maxAsteroidsToSpawn - minAsteroidsToSpawn) + minAsteroidsToSpawn);

			spamAsteroids (50.0f, asteroidsToSpawn);
		}

		//spam the first astaeroids
		if (level.StartedAsteroids == 2)
		{
			spamAsteroids(10.0f, 30);
			level.StartedAsteroids++;
		}

		//counter
		if (level.StartedAsteroids < 2) 
		{
			level.StartedAsteroids=level.StartedAsteroids+1;
		}
	}
	#endregion

	#region User Methods

	/// <summary>
	/// Establishs the asteoid pools.
	/// </summary>
	public void establishAsteoidPools()
	{
		asteroids = new List<GameObject>[asteroidsPrefabs.Count];

		for (int i = 0; i < asteroidsPrefabs.Count; i++) 
		{
			asteroids[i] = new List<GameObject>();

			for (int j = 0; j < numAsteroids; j++) 
			{
				asteroids [i].Add (Instantiate (asteroidsPrefabs [i]));
				asteroids[i][j].GetComponent<AsteroidCollisionManager>().AsteroidMgr = this;
				asteroids [i] [j].SetActive (false);
			}
		}
	}
		
	/// <summary>
	/// Check if the asteroid is out of the camera sight.
	/// </summary>
	/// <param name="asteroid"></param>
	/// <returns></returns>
	bool CheckOutOfCamera(GameObject asteroid)
	{
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(asteroid.transform.position);
		//return (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)&&screenPoint.z<0;
        return (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1) /*&& screenPoint.z < 0*/;
    }

    /// <summary>
    /// Check if the asterois is more than 10 spaces away from the player.
    /// </summary>
    /// <param name="asteroid"></param>
    /// <returns></returns>
    bool CheckFarEnoughFromThePlayer(GameObject asteroid)
    {
        float distanceToCheck = 10;
        float distanceToPlayer = (asteroid.transform.position - spaceShip.transform.position).magnitude;
        return distanceToPlayer > distanceToCheck;
    }

	/// <summary>
	/// Check if the asteroid is out of the camera sight and far away from the player
	/// </summary>
	void CheckAsteroids()
	{
		for(int i = 0; i < asteroidsPrefabs.Count; i++)
		{
			for (int j = 0; j < numAsteroids; j++) 
			{
				// Check if any of them is out of screen
				if (asteroids[i][j].activeInHierarchy && CheckOutOfCamera(asteroids[i][j]) && CheckFarEnoughFromThePlayer(asteroids[i][j])) 
				{
					asteroids[i][j].SetActive(false);
                    currentActiveAsteroids--;
				}
			}
		}
	}
		
	/// <summary>
	/// Determine the zone in which the asteroids should appears.
	/// </summary>
	/// <param name="dist"></param>
	/// <returns></returns>
	Vector3 DetermineZoneToAppear(float dist)
	{
		float randomX = Random.value;
		float randomY = Random.value;

        randomY = Random.Range(0.3f, 0.7f);

        float zDistance = (spaceShip.transform.position - Camera.main.transform.position).magnitude;
		float randomZ = (Random.value * 30.0f) + zDistance + dist;
		Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, randomZ));
		return worldPoint;
	}


	/// <summary>
	/// Activate the asteroids from the pool, so they appear in the screen.
	/// </summary>
	/// <param name="index"><param name="dist"></param>
	public void ActivateAsteroidFromPool(int index, float dist)
	{
		for(int i = 0; i < asteroids[index].Count; i++)
		{
			if (!asteroids[index][i].activeInHierarchy)
			{
				asteroids[index][i].SetActive(true);
				asteroids[index][i].transform.position = DetermineZoneToAppear(dist);
				Rigidbody rb=asteroids [index] [i].GetComponent<Rigidbody> ();
				rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
				addRandomTorque(rb);
				addRandomForce(rb);
				AsteroidCollisionManager asteroidColl=asteroids [index] [i].GetComponent<AsteroidCollisionManager> ();
				asteroidColl.ResetCounter ();
				asteroidColl.RestartHealth();
                currentActiveAsteroids++;
				return;
			}
		}
	}

	/// <summary>
	/// Activates the asteroid after a division.
	/// </summary>
	/// <param name="index">Index.</param>
	/// <param name="pos">Position.</param>
	public void ActivateAsteroidFromDivision(int index, Vector3 pos)
	{
		for(int i = 0; i < asteroids[index].Count; i++)
		{
			if (!asteroids[index][i].activeInHierarchy)
			{
				asteroids[index][i].SetActive(true);
				asteroids[index][i].transform.position = pos;
				Rigidbody rb=asteroids [index] [i].GetComponent<Rigidbody> ();
				rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
				addRandomTorque(rb);
				addRandomForce(rb);
				AsteroidCollisionManager asteroidColl=asteroids [index] [i].GetComponent<AsteroidCollisionManager> ();
				asteroidColl.ResetCounter ();
				asteroidColl.RestartHealth();
                currentActiveAsteroids++;
				return;
			}
		}
    }
		
    /// <summary>
    /// Spams the asteroids randomly.
    /// </summary>
    /// <param name="dist">Dist.</param>
    /// <param name="numAsteroids">Number asteroids.</param>
	public void spamAsteroids(float dist, int numAsteroids)
	{
		for (int i = 0; i < numAsteroids; i++) 
		{
			int randomIndex = (int)(Random.value * asteroidsPrefabs.Count);
			ActivateAsteroidFromPool (randomIndex, dist);
		}
	}

	/// <summary>
	/// Adds the random torque to the asteroids.
	/// </summary>
	/// <param name="rb">Rb.</param>
	void addRandomTorque(Rigidbody rb)
	{
		float randomX = Random.value * 10 - 5;
		float randomY = Random.value * 10 - 5;
		float randomZ = Random.value * 10 - 5;

		rb.AddTorque(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
	}

	/// <summary>
	/// Adds the random force to the asteroids.
	/// </summary>
	/// <param name="rb">Rb.</param>
	void addRandomForce(Rigidbody rb)
	{
		float randomX = Random.value * 10 - 5;
		float randomY = Random.value * 10 - 5;
		float randomZ = Random.value * 10 - 5;

		rb.AddForce(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
	}


    #endregion
}