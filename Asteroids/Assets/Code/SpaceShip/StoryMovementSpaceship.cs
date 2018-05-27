using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMovementSpaceship : BaseSpaceship {

	#region Public Attributes
	#endregion

	#region Private Attributes
	private StoryLevelManager levelManager;
	#endregion

	#region Properties Attributes

	#endregion

	#region MonoDevelop Methods

	// Use this for initialization
	protected override void Start () 
	{
		base.Start ();
		levelManager = FindObjectOfType<StoryLevelManager>();
        posCurrentWayPoint = Vector3.zero; 
	}

	// Update is called once per frame
	protected override void Update () 
	{

        if (destroyed) return;

        velocity = rb.velocity;
		currentUpdateTime += Time.deltaTime;
		rb.velocity = Vector3.ClampMagnitude (rb.velocity , maxSpeed);

		Movement ();

		NextWaypoint ();

		VerticalSpeedControl ();
	}
	#endregion


	#region User Methods
	/// <summary>
	/// Catch the next waypoint
	/// </summary>
	protected override void NextWaypoint()
	{
		if ((transform.position - posCurrentWayPoint).magnitude <= 2) 
		{
			levelManager.ReachWaypoint();
			GameObject nextWaypoint = levelManager.CurrentWaypoint;
		}
	}
	/// <summary>
	/// Movement of the Story mode you go fordward until a hook appear
	/// </summary>
	protected override void Movement()
	{
		if (levelManager.CurrentWaypoint != null) 
		{
			posCurrentWayPoint = levelManager.CurrentWaypoint.transform.position;

			posNextWayPointRelative = posCurrentWayPoint - transform.position;

			adjustedDirection = AdjustDirection (velocity, posNextWayPointRelative).normalized;
			rb.AddForce (adjustedDirection * force, ForceMode.Impulse);

			previousRotation = nextRotation;
			nextRotation = Quaternion.LookRotation (posNextWayPointRelative);
			currentUpdateTime = 0;

		} 
		else 
		{
			rb.AddForce (transform.forward * force, ForceMode.Impulse);
		}

		transform.rotation = Quaternion.Slerp (transform.rotation, nextRotation, 0.1f);
	}

	/// <summary>
	/// Check collision with bounds of map
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Limit")
		{
			nextRotation = Quaternion.LookRotation (-transform.forward);
		}
	}

	#endregion
}
