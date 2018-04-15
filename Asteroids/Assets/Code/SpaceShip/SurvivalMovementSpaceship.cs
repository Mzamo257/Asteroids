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
		velocity = rb.velocity;
		posNextWayPointRelative = posCurrentWayPoint - transform.position;

		currentUpdateTime += Time.deltaTime;

		angleResultant = Vector3.Angle (velocity, posNextWayPointRelative);

		rb.velocity = Vector3.ClampMagnitude (rb.velocity , maxSpeed);

		//Hay que arreglar este que es el primer empujon de la nave
		if (!firstMovement) {
			rb.AddForce (posNextWayPointRelative.normalized * force, ForceMode.Impulse);
			firstMovement = true;
			previousRotation = nextRotation;
			nextRotation = Quaternion.LookRotation (posNextWayPointRelative);
		} 
		else if(secondMovement && currentUpdateTime > updateTime)
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
	#endregion

	#region User Methods
	#endregion
}
