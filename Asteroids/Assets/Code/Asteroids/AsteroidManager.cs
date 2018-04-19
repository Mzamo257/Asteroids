using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
	#region Public Attributes
	public List<GameObject> asteroidsPrefabs;
	public int numAsteroids;
	public int type;
	#endregion

	#region Private Attributes
	// private List<GameObject> asteroids;
	private List<GameObject>[] asteroids;
	private GameObject spaceShip;
	private float counter;
	private int started=0;
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Start () {
		// Method 1: 
		// Asteroids in the scene by hand and got by FindGameObjectsWithTag
		//asteroidsPrefabs = GameObject.FindGameObjectsWithTag("Asteroid");
		//
		spaceShip = GameObject.Find("Viper");
        establishAsteoidPools();

		//spamAsteroids(10.0f, 100);
        //startAsteroids();
	}

	// Update is called once per frame
	void Update () {
		// TODO: Mover este chequeo a una corrutina
		counter += Time.deltaTime;
		if (counter >= 0.01) {
			CheckAsteroids();
			counter = 0;
            Vector3 posSpaceShip = spaceShip.transform.position;
			if (started > 2) {
				spamAsteroids (50.0f, 100);
			}

			if (started==2)
			{
				spamAsteroids(10.0f, 100);
				Debug.Log ("holi");
			}
		}
		
		started++;
	}
	#endregion

	#region User Methods

	/// <summary>
	/// Establishs the asteoid pools.
	/// </summary>
	public void establishAsteoidPools()
	{
		asteroids = new List<GameObject>[asteroidsPrefabs.Count];

		for (int i = 0; i < asteroidsPrefabs.Count; i++) {
			asteroids[i] = new List<GameObject>();
			for (int j = 0; j < numAsteroids; j++) {
				asteroids [i].Add (Instantiate (asteroidsPrefabs [i]));
				asteroids[i][j].GetComponent<AsteroidCollisionManager>().AsteroidMgr = this;
				asteroids [i] [j].SetActive (false);
			}
		}
	}
		
	/// <summary>
	/// Check if the asteroid is out of the camera
	/// </summary>
	/// <param name="asteroid"></param>
	/// <returns></returns>
	bool CheckOutOfCamera(GameObject asteroid)
	{
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(asteroid.transform.position);
		return screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;
	}
		
	/// <summary>
	/// Haz una pasada comprobando los asteriodes
	/// De momento que estén en pantalla
	/// </summary>
	void CheckAsteroids()
	{
		for(int i = 0; i < asteroidsPrefabs.Count; i++)
		{
			for (int j = 0; j < numAsteroids; j++) {
				// Check if any of them is out of screen
				if (asteroids[i][j].activeInHierarchy && CheckOutOfCamera(asteroids[i][j])) {

					asteroids[i][j].SetActive(false);
				}
			}
		}
	}
		
	/// <summary>
	/// Determine the zone in which the asteroids should appears
	/// </summary>
	/// <param name="dist"></param>
	/// <returns></returns>
	Vector3 DetermineZoneToAppear(float dist)
	{
		float randomX = Random.value;
		float randomY = Random.value;
		float zDistance = (spaceShip.transform.position - Camera.main.transform.position).magnitude;
		float randomZ = (Random.value * 70.0f) + zDistance + dist;
		Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, randomZ));
		//Vector3 worldPoint = Camera.main.transform.position + randomZ * Camera.main.transform.forward + randomX * Camera.main.transform.right + randomY * Camera.main.transform.up;
		return worldPoint;
	}

	/// <summary>
	/// Activate the asteroids from the pool, so they appear in the screen
	/// </summary>
	/// <param name="index"><param name="dist"></param>
	public void ActivateAsteroid(int index, float dist)
	{
		for(int i = 0; i < asteroids[index].Count; i++)
		{
			if (!asteroids[index][i].activeInHierarchy){
				asteroids[index][i].SetActive(true);
				asteroids[index][i].transform.position = DetermineZoneToAppear(dist);
				asteroids[index][i].GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
				return;
			}
		}
		// Debug.Log("All asteroids in use");
	}
		
    /// <summary>
    /// Spams the asteroids aleatory.
    /// </summary>
    /// <param name="dist">Dist.</param>
    /// <param name="numAsteroids">Number asteroids.</param>
	public void spamAsteroids(float dist, int numAsteroids){
		for (int i = 0; i < numAsteroids; i++) {
			int randomIndex = (int)(Random.value * asteroidsPrefabs.Count);
			ActivateAsteroid (randomIndex, dist);
		}
	}


    #endregion
}