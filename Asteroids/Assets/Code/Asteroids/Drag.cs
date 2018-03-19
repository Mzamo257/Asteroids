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
    public Vector3 newCenterOfMass;
    public Vector3 impulse;
	//public Vector3 torque;

	// Use this for initialization
	void Start () {
		selected = false;
		rb = gameObject.GetComponent<Rigidbody> ();
        rb.centerOfMass = newCenterOfMass;
        addRandomTorque();
        addRandomForce();
		//rb.AddTorque(torque, ForceMode.Impulse);
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
		//Debug.Log (direction + ", " + dist);
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
        float randomX = Random.value;
        float randomY = Random.value;
        float randomZ = Random.value;

        rb.AddForce(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
//        Debug.Log(new Vector3(randomX, randomY, randomZ));
    }

}

