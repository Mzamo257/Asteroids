using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMovementSpaceship : BaseSpaceship {

	#region Public Attributes
	#endregion

	#region Private Attributes
	private StoryLevelManager levelManager;
	private float angleResultant;

	private int counterWaypoints;
	#endregion

	#region Properties Attributes

	#endregion

	#region MonoDevelop Methods

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		levelManager = FindObjectOfType<StoryLevelManager>();
        posCurrentWayPoint = Vector3.zero; 
		counterWaypoints = 0;
	}

	// Update is called once per frame
	protected override void Update () {

		velocity = rb.velocity;
		currentUpdateTime += Time.deltaTime;
		rb.velocity = Vector3.ClampMagnitude (rb.velocity , maxSpeed);

		Movement ();

		NextWaypoint ();

		VerticalSpeedControl ();
	}

	#endregion


	#region User Methods

	protected override void NextWaypoint()
	{
		if ((transform.position - posCurrentWayPoint).magnitude <= 2) {

			levelManager.ReachWaypoint();
			GameObject nextWaypoint = levelManager.CurrentWaypoint;
		}
	}

	protected override void Movement()
	{
		if (levelManager.CurrentWaypoint != null) {
			posCurrentWayPoint = levelManager.CurrentWaypoint.transform.position;

			posNextWayPointRelative = posCurrentWayPoint - transform.position;

			angleResultant = Vector3.Angle (velocity, posNextWayPointRelative);

			adjustedDirection = AdjustDirection (velocity, posNextWayPointRelative).normalized;
			rb.AddForce (adjustedDirection * force, ForceMode.Impulse);

			previousRotation = nextRotation;
			nextRotation = Quaternion.LookRotation (posNextWayPointRelative);
			currentUpdateTime = 0;

		} else {
			rb.AddForce (transform.forward * force, ForceMode.Impulse);
			if (rb.velocity.magnitude > Vector3.zero.magnitude) {
				//nextRotation = Quaternion.LookRotation (rb.velocity);
			}
		}

		transform.rotation = Quaternion.Slerp (transform.rotation, nextRotation, 0.1f);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Limit")
		{
			nextRotation = Quaternion.LookRotation (-transform.forward);
		}
	}

	#endregion
}
