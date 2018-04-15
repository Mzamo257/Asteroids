using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpaceship : MonoBehaviour {

	#region Public Attributes
	//public Transform nextWayPoint;
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
	protected Vector3 adjustDirection(Vector3 pos, Vector3 obj)
	{
		return (obj - pos);
	}

	/*void SetNextWayPoint()
	{
		nextWayPoint = level_manager.getWaypoint (current_wayPoint).GetComponent<WayPoint>().nextWayPoint;
	}*/
	#endregion
}
