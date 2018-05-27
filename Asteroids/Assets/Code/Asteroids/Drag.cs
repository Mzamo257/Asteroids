using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {
	
	#region Public Attributes
	public float force;
	public Material asteroid;
	public Material cought;
	public Vector3 impulse;

	#endregion

	#region Private Attributes
	private Vector3 dist;
	private float posX;
	private float posY;
	private bool selected;
	private Rigidbody rb;
	private BaseLevelManager levelManager;
    private AsteroidManager asteroidManager;
    #endregion

    #region MonoDevelop Methods
    void Start () 
	{
		selected = false;
		rb = gameObject.GetComponent<Rigidbody> ();
		levelManager = FindObjectOfType<BaseLevelManager> ();
        asteroidManager = FindObjectOfType<AsteroidManager>();
    }

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonUp (0) && selected) 
		{
			selected = false;

            if (levelManager != null)
            {
                asteroidManager.AsteroidLineRenderer.gameObject.SetActive(false);
                levelManager.AsteroidSelected = false;
            }
			gameObject.GetComponent<MeshRenderer> ().material = asteroid;
			LaunchAsteroid();
		}
        //
        if (selected) ArrowDirection();
	}
	#endregion

	#region User Methods
	/// <summary>
	/// Raises the mouse down event.
	/// </summary>
	void OnMouseDown()
	{
		if (levelManager.currentState == GameState.InGame) 
		{
			selected = true;
			asteroidManager.AsteroidLineRenderer.gameObject.SetActive (true);
			gameObject.GetComponent<MeshRenderer> ().material = cought;
			if (levelManager != null) 
			{
				levelManager.AsteroidSelected = true;
				levelManager.asteroidPosition (transform.position);
			}
		}
	}

	/// <summary>
	/// Launchs the asteroid, depending how far you have the pointer from the asteroid it will be launched with more force.
	/// </summary>
	void LaunchAsteroid()
	{
		Vector3 objectPos = transform.position;
		float zToUse = (objectPos - Camera.main.transform.position).magnitude;
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, zToUse));
        
		Vector3 direction = mouseWorldPos - objectPos;
		rb.AddForce(direction*force, ForceMode.Impulse);
	}

	/// <summary>
	/// Makes a LineRenderer that draws a line between the asteroid and the pointer.
	/// </summary>
    void ArrowDirection()
    {
        Vector3 objectPos = transform.position;
        float zToUse = (objectPos - Camera.main.transform.position).magnitude;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zToUse));

        asteroidManager.AsteroidLineRenderer.SetPosition(0, objectPos);
        asteroidManager.AsteroidLineRenderer.SetPosition(1, mouseWorldPos);
    }

	#endregion
}

