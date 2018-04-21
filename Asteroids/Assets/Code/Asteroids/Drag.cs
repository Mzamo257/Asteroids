using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {
	
	#region Public Attributes
	public float force;
	public Material cubo1;
	public Material cubo2;
	public Vector3 newCenterOfMass;
	public Vector3 impulse;

	//public Vector3 torque;

	#endregion

	#region Private Attributes
	private Vector3 dist;
	private float posX;
	private float posY;
	private bool selected;
	private Rigidbody rb;
	private BaseLevelManager levelManager;
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Start () {
		selected = false;
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.centerOfMass = newCenterOfMass;
		addRandomTorque();
		addRandomForce();
		levelManager = FindObjectOfType<BaseLevelManager> ();
		//rb.AddTorque(torque, ForceMode.Impulse);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0) && selected) {
			selected = false;
			levelManager.AsteroidSelected = false;
			gameObject.GetComponent<MeshRenderer> ().material = cubo1;
			launchAsteroid();
		}
	}
	#endregion

	#region User Methods
	void OnMouseDown(){
		selected = true;
		gameObject.GetComponent<MeshRenderer> ().material = cubo2;
		levelManager.AsteroidSelected = true;
		levelManager.asteroidPosition (transform.position);
	}

	void launchAsteroid(){
		Vector3 objectPos = transform.position;
		float zToUse = (objectPos - Camera.main.transform.position).magnitude;
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, zToUse));
		Vector3 direction = mouseWorldPos - objectPos;
		float dist = direction.magnitude;
		//Debug.Log (direction + ", " + dist);
		//rb.velocity=Vector3.zero;
		rb.AddForce(direction*force, ForceMode.Impulse);
	}

	void addRandomTorque()
	{
		float randomX = Random.value * 10 - 5;
		float randomY = Random.value * 10 - 5;
		float randomZ = Random.value * 10 - 5;

		rb.AddTorque(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
	}

	void addRandomForce()
	{
		float randomX = Random.value * 10 - 5;
		float randomY = Random.value * 10 - 5;
		float randomZ = Random.value * 10 - 5;

		rb.AddForce(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
		//        Debug.Log(new Vector3(randomX, randomY, randomZ));
	}
	#endregion
}

