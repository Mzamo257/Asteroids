using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpaceship : MonoBehaviour {

	#region Public Attributes
	public float updateTime;
	public float force;
	public float maxSpeed;
	#endregion

	#region Private Attributes
	protected Rigidbody rb;
	protected bool secondMovement = false;
	protected bool firstMovement = false;
	protected float currentUpdateTime;

	protected Vector3 adjustedDirection; // Here for testing
	protected Quaternion previousRotation;
	protected Quaternion nextRotation;
	protected Vector3 velocity;
	protected Vector3 posNextWayPointRelative;
	protected Vector3 posCurrentWayPoint;
	#endregion

	#region Properties Attributes
	#endregion

	#region MonoDevelop Methods

	// Use this for initialization
	protected virtual void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		previousRotation = new Quaternion();
		nextRotation = new Quaternion();
	}

	// Update is called once per frame
	protected virtual void Update () {
	}

	#endregion


	#region User Methods
	protected virtual void Movement()
	{
		
	}

	protected virtual void NextWaypoint()
	{
		
	}

	protected virtual void Dodge()
	{
		
	}

	protected Vector3 AdjustDirection(Vector3 pos, Vector3 obj)
	{
		return (obj - pos);
	}

	protected void VerticalSpeedControl()
	{
		if (rb.velocity.y > 0.1f) {
			rb.AddForce (Vector3.down, ForceMode.Force);
		} else if (rb.velocity.y < -0.1) {
			rb.AddForce (Vector3.up, ForceMode.Force);
		}
	}

	#endregion
}
