using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
    #region Public Attributes
    public List<GameObject> asteroidsPrefabs;
    public int numAsteroids;
    public GameObject asteroidLineRendererPrefab;
    #endregion

    #region Private Attributes
    // private List<GameObject> asteroids;
    private List<GameObject>[] asteroids;
    private GameObject spaceShip;
    private float counter;
    private int started = 0;
    private BaseLevelManager level;
    private LineRenderer asteroidLineRenderer;
    #endregion

    #region Properties
    public LineRenderer AsteroidLineRenderer { get { return asteroidLineRenderer; } }
    #endregion

    #region MonoDevelop Methods
    // Use this for initialization
    void Start () {
		spaceShip = GameObject.Find("Viper");
        establishAsteoidPools();
		level = FindObjectOfType<BaseLevelManager>();
        //
        GameObject instantiatedAsteroidLineRenderer = Instantiate(asteroidLineRendererPrefab, Vector3.zero, Quaternion.identity);
        asteroidLineRenderer = instantiatedAsteroidLineRenderer.GetComponent<LineRenderer>();
        instantiatedAsteroidLineRenderer.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter >= 0.3) {
			CheckAsteroids();
			counter = 0;
            //Vector3 posSpaceShip = spaceShip.transform.position;
		}

		if (level.StartedAsteroids == 4) {
			spamAsteroids (50.0f, 5);
		}

		if (level.StartedAsteroids==2)
		{
			spamAsteroids(10.0f, 30);
			level.StartedAsteroids++;
			//Debug.Log ("holi");
		}
		if (level.StartedAsteroids < 2) {
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
		return (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)&&screenPoint.z<0;
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
		float randomZ = (Random.value * 30.0f) + zDistance + dist;
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
				Rigidbody rb=asteroids [index] [i].GetComponent<Rigidbody> ();
				rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
				addRandomTorque(rb);
				addRandomForce(rb);
				asteroids [index] [i].GetComponent<AsteroidCollisionManager> ().ResetCounter ();
				return;
			}
		}
		// Debug.Log("All asteroids in use");
	}

	public void ActivateAsteroidFromDivision(int index, Vector3 pos)
	{
		for(int i = 0; i < asteroids[index].Count; i++)
		{
			//Debug.Log ("que pasa wey");
			if (!asteroids[index][i].activeInHierarchy){
				asteroids[index][i].SetActive(true);
				asteroids[index][i].transform.position = pos;
				Rigidbody rb=asteroids [index] [i].GetComponent<Rigidbody> ();
				rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
				//Debug.Log ("lanzado");
				addRandomTorque(rb);
				addRandomForce(rb);
				asteroids [index] [i].GetComponent<AsteroidCollisionManager> ().ResetCounter ();
				return;
			}
			
		}
        //Debug.Log("Pool vacia");
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

	void addRandomTorque(Rigidbody rb)
	{
		float randomX = Random.value * 10 - 5;
		float randomY = Random.value * 10 - 5;
		float randomZ = Random.value * 10 - 5;

		rb.AddTorque(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
	}

	void addRandomForce(Rigidbody rb)
	{
		float randomX = Random.value * 10 - 5;
		float randomY = Random.value * 10 - 5;
		float randomZ = Random.value * 10 - 5;

		rb.AddForce(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
	}


    #endregion
}