using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalMovementSpaceship : BaseSpaceship {

	#region Public Attribute

	#endregion

	#region Private Attributes
	private SurvivalLevelManager levelManager;
	private float angleResultant;
	private float maxDetectionDistance = 10.0f;
	private RaycastHit hitInfo;
	#endregion

	#region Properties Attributes
	#endregion

	#region MonoDevelop Methods

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		levelManager = GameObject.Find("Level Manager").GetComponent<SurvivalLevelManager>();
		posCurrentWayPoint = levelManager.CurrentWaypoint.transform.position;
	}

	// Update is called once per frame
	protected override void Update () {
		
		dodge ();
		velocity = rb.velocity;
		currentUpdateTime += Time.deltaTime;
		angleResultant = Vector3.Angle (velocity, posNextWayPointRelative);
		rb.velocity = Vector3.ClampMagnitude (rb.velocity , maxSpeed);

		posNextWayPointRelative = posCurrentWayPoint - transform.position;

		Redirect ();
		VerticalSpeedControl ();
		NextWaypoint ();

	}
	#endregion

	#region User Methods
	private void NextWaypoint()
	{
		if ((transform.position - posCurrentWayPoint).magnitude <= 2) {

			levelManager.AdvanceWaypoint();
			GameObject nextWaypoint = levelManager.CurrentWaypoint;

			if (nextWaypoint != null)
			{
				posCurrentWayPoint = nextWaypoint.transform.position;
			}

			firstMovement = true;
			secondMovement = true;
		}
	}

	private void dodge()
	{
		if (Physics.SphereCast (transform.position, maxDetectionDistance, rb.velocity, out hitInfo)) {
			if(hitInfo.transform.tag.Equals("Asteroid"))
			{
				Debug.Log ("Sphere Detection");
				if (Physics.Raycast (transform.position, rb.velocity, out hitInfo, maxDetectionDistance)) {

					if(hitInfo.transform.tag.Equals("Asteroid"))
					{
						int random = UnityEngine.Random.Range (0,1);
						if (random == 0) {
							rb.AddForce (transform.up, ForceMode.Impulse);
						} else if(random == 1){
							rb.AddForce (-transform.up, ForceMode.Impulse);
						}
							
						Debug.Log ("Raycast Detection");
					}
				}
			}
		}
	}

	private void Redirect()
	{
		
		if(currentUpdateTime > updateTime)
		{
			adjustedDirection = adjustDirection(velocity, posNextWayPointRelative).normalized;
			rb.AddForce (adjustedDirection * force, ForceMode.Impulse);

			// TODO: Guardar un valor de orientación
			// Y hacer una transición más limpia con Slerp

			previousRotation = nextRotation;
			nextRotation = Quaternion.LookRotation (posNextWayPointRelative);
			currentUpdateTime = 0;
		}

		transform.rotation = Quaternion.Slerp (previousRotation, nextRotation, currentUpdateTime);

	}
		

	private void VerticalSpeedControl()
	{
		if (rb.velocity.y > 0.1f) {
			rb.AddForce (Vector3.down, ForceMode.Force);
			Debug.Log ("VerticalSpeedControl_hacia abajo");
		} else if (rb.velocity.y < -0.1) {
			rb.AddForce (Vector3.up, ForceMode.Force);
			Debug.Log ("VerticalSpeedControl_ hacia arriba");

		}
	}
	#endregion
}
