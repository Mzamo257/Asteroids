using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {
	Vector3 dist;
	float posX;
	float posY;
	bool selected;
	public Material cubo1;
	public Material cubo2;
	private Rigidbody rb;
	public float force=0.1f;
	//public float torque;

	// Use this for initialization
	void Start () {
		selected = false;
		rb = gameObject.GetComponent<Rigidbody> ();
		//rb.AddTorque(100, 10, 10);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0) && selected) {
			selected = false;
			gameObject.GetComponent<MeshRenderer> ().material = cubo1;
			launchAsteroid();
		}
	}

	void OnMouseDown(){
		selected = true;
		gameObject.GetComponent<MeshRenderer> ().material = cubo2;
	}

	void launchAsteroid(){
		Vector3 objectPos = transform.position;
		float zToUse = (objectPos - Camera.main.transform.position).magnitude;
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, zToUse));
		Vector3 direction = mouseWorldPos - objectPos;
		float dist = direction.magnitude;
		Debug.Log (direction + ", " + dist);
		rb.AddForce(direction*force, ForceMode.Impulse);
	}
}

