﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMovementSpaceship : BaseSpaceship {

	#region Public Attributes
	#endregion

	#region Private Attributes
	private StoryLevelManager level_manager;
	private float angleResultant;
	#endregion

	#region Properties Attributes
	#endregion

	#region MonoDevelop Methods

	// Use this for initialization
	protected override void Start () {

		base.Start ();
		level_manager = FindObjectOfType<StoryLevelManager>();
        posCurrentWayPoint = Vector3.zero; /*level_manager.CurrentWaypoint.transform.position;*/
	}

	// Update is called once per frame
	protected override void Update () {
		velocity = rb.velocity;
		posNextWayPointRelative = posCurrentWayPoint - transform.position;

		currentUpdateTime += Time.deltaTime;

		angleResultant = Vector3.Angle (velocity, posNextWayPointRelative);

		rb.velocity = Vector3.ClampMagnitude (rb.velocity , maxSpeed);

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

		//		Debug.Log ("velocity" + rb.velocity);
		transform.rotation = Quaternion.Slerp (previousRotation, nextRotation, currentUpdateTime);

		Debug.DrawRay (transform.position, rb.velocity, Color.red);
		Debug.DrawRay (transform.position, adjustDirection (velocity, posNextWayPointRelative) , Color.green);
		Debug.DrawRay (transform.position, posNextWayPointRelative, Color.white);

		if ((transform.position - posCurrentWayPoint).magnitude <= 2) {

			//Este deberá de ser el de la collección de waypoint del levelManager del story
			//si ese array esta lleno la direccion hacia el del numero 0
			//si esta vacio sigo con la velocidad que llevaba y direccion
			//level_manager.AdvanceWaypoint();
		//	GameObject nextWaypoint = level_manager.CurrentWaypoint;

			/*if (nextWaypoint != null)
			{
				pos_current_wayPoint = nextWaypoint.transform.position;
			}*/

			firstMovement = true;
			secondMovement = true;
		}
	}

	#endregion


	#region User Methods
	#endregion
}