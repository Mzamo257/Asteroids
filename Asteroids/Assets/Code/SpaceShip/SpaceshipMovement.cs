using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour {


	#region Public Attributes
	//public Transform nextWayPoint;
	public float updateTime;
	public float force;
	public float maxSpeed;

	#endregion

	#region Private Attributes
	private level_Manager level_manager;
	private int current_wayPoint = 0;
	private Vector3 pos_current_wayPoint;
	private Rigidbody rb;
	private bool second_Movement = false;
	private bool first_Movement = false;
	private float currentUpdateTime;

	private Vector3 adjustedDirection; // Here for testing

	private Quaternion previousRotation;
	private Quaternion nextRotation;
	#endregion

	#region MonoDevelop Methods

	// Use this for initialization
	void Start () {

		rb = gameObject.GetComponent<Rigidbody> ();
		previousRotation = new Quaternion();
		nextRotation = new Quaternion();
		pos_current_wayPoint = level_manager.getWaypoint (current_wayPoint).transform.position;
	}

	// Update is called once per frame
	void Update () {
		Vector3 velocity = rb.velocity;
		Vector3 posNextWayPoint_Relative = pos_current_wayPoint - transform.position;

		currentUpdateTime += Time.deltaTime;

		float Angle_resultant = Vector3.Angle (velocity, posNextWayPoint_Relative);

		//Debug.Log (Angle_resultant);

		rb.velocity = Vector3.ClampMagnitude (rb.velocity , maxSpeed);

		if (!first_Movement) {
			rb.AddForce (posNextWayPoint_Relative.normalized * force, ForceMode.Impulse);
			first_Movement = true;
			previousRotation = nextRotation;
			nextRotation = Quaternion.LookRotation (posNextWayPoint_Relative);
		} 
		else if(second_Movement && currentUpdateTime > updateTime)
		{
			adjustedDirection = adjust_Direction(velocity, posNextWayPoint_Relative).normalized;
			rb.AddForce (adjustedDirection * force, ForceMode.Impulse);

			// TODO: Guardar un valor de orientación
			// Y hacer una transición más limpia con Slerp

			previousRotation = nextRotation;
			nextRotation = Quaternion.LookRotation (posNextWayPoint_Relative);
			currentUpdateTime = 0;
		}

//		Debug.Log ("velocity" + rb.velocity);
		transform.rotation = Quaternion.Slerp (previousRotation, nextRotation, currentUpdateTime);

		//Debug.DrawRay (transform.position, rb.velocity, Color.red);
		//Debug.DrawRay (transform.position, adjust_Direction (velocity, posNextWayPoint_Relative) , Color.green);
		//Debug.DrawRay (transform.position, posNextWayPoint_Relative, Color.white);

		if ((transform.position - pos_current_wayPoint).magnitude <= 2) {			
			//SetNextWayPoint();

			current_wayPoint++;
			pos_current_wayPoint = level_manager.getWaypoint (current_wayPoint).transform.position;

			first_Movement = true;
			second_Movement = true;
		}
	}

	#endregion


	#region User Methods
	Vector3 adjust_Direction(Vector3 pos, Vector3 obj)
	{
		return (obj - pos);
	}

	/*void SetNextWayPoint()
	{
		nextWayPoint = level_manager.getWaypoint (current_wayPoint).GetComponent<WayPoint>().nextWayPoint;
	}*/
	#endregion
}
