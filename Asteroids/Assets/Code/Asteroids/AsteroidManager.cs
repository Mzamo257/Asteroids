using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
	#region Public Attributes
	public List<GameObject> asteroidsPrefabs;
	public int numAsteroids;
	#endregion

	#region Private Attributes
	// private List<GameObject> asteroids;
	private List<GameObject>[] asteroids;
	private GameObject spaceShip;
	private float counter;
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
        //startAsteroids();
	}

	// Update is called once per frame
	void Update () {
		// TODO: Mover este chequeo a una corrutina
		counter += Time.deltaTime;
		if (counter >= 0.01) {
			CheckAsteroids();
			counter = 0;
            Vector3 posSpaceShip = Camera.main.WorldToViewportPoint(spaceShip.transform.position);
            // TODO: Revise that
            if (posSpaceShip.z==0)
            {
                startAsteroids();
            }
			spamAsteroids ();
		}
	}
	#endregion

	#region User Methods

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
	/// 
	/// </summary>
	/// <returns></returns>
	Vector3 DetermineZoneToAppear(float dist)
	{
		float randomX = Random.value;
		float randomY = Random.value;
		float zDistance = (spaceShip.transform.position - Camera.main.transform.position).magnitude;
		float randomZ = (Random.value * 70.0f) + zDistance + dist;
		Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, randomZ));
		return worldPoint;
	}

	/// <summary>
	/// 
	/// </summary>
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

	public void establishAsteoidPools()
	{
		asteroids = new List<GameObject>[asteroidsPrefabs.Count];

		for (int i = 0; i < asteroidsPrefabs.Count; i++) {
            asteroids[i] = new List<GameObject>();
            for (int j = 0; j < numAsteroids; j++) {
				asteroids [i].Add (Instantiate (asteroidsPrefabs [i]));
				asteroids [i] [j].SetActive (false);
			}
		}
	}
    
    //spamea asteroides aleatorios
	public void spamAsteroids(){
		for (int i = 0; i < 40; i++) {
			int randomIndex = (int)(Random.value * asteroidsPrefabs.Count);
            Debug.Log(asteroids[randomIndex].Count);
			ActivateAsteroid (randomIndex, 30.0f);
		}
	}

    public void startAsteroids(){
        for (int i = 0; i < 10; i++)
        {
            int randomIndex = (int)(Random.value * asteroidsPrefabs.Count);
            Debug.Log(asteroids[randomIndex].Count);
            ActivateAsteroid(randomIndex, 10.0f);
        }
    }

    #endregion
}