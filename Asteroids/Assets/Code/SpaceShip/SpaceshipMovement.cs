﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipMovement : BaseSpaceship {

	#region Public Attribute

	#endregion

	#region Private Attributes
	private SurvivalLevelManager level_manager;
	private Vector3 pos_current_wayPoint;
	#endregion

	#region Properties Attributes
	#endregion

	#region MonoDevelop Methods

	// Use this for initialization
	protected override void Start () {

		base.Start ();
		level_manager = GameObject.Find("Level Manager").GetComponent<SurvivalLevelManager>();
		pos_current_wayPoint = level_manager.CurrentWaypoint.transform.position;
	}

	// Update is called once per frame
	protected override void Update () {
		velocity = rb.velocity;
		Vector3 posNextWayPoint_Relative = pos_current_wayPoint - transform.position;

		currentUpdateTime += Time.deltaTime;

		float Angle_resultant = Vector3.Angle (velocity, posNextWayPoint_Relative);

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

		transform.rotation = Quaternion.Slerp (previousRotation, nextRotation, currentUpdateTime);

		if ((transform.position - pos_current_wayPoint).magnitude <= 2) {

			level_manager.AdvanceWaypoint();
			GameObject nextWaypoint = level_manager.CurrentWaypoint;

			if (nextWaypoint != null)
			{
				pos_current_wayPoint = nextWaypoint.transform.position;
			}

			first_Movement = true;
			second_Movement = true;
		}
	}
	#endregion


	#region User Methods
	#endregion
}
