using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMovementSpaceship : BaseSpaceship {

	#region Public Attributes
	//public Transform nextWayPoint;
	public float updateTime;
	public float force;
	public float maxSpeed;

	#endregion

	#region Private Attributes
	private StorylLevelManager level_manager;
	private Vector3 pos_current_wayPoint;

	#endregion

	#region Properties Attributes
	#endregion

	#region MonoDevelop Methods

	// Use this for initialization
	protected override void Start () {

		base.Start ();
		level_manager = GameObject.Find("Level Manager").GetComponent<StoryLevelManager>();
		pos_current_wayPoint = level_manager.CurrentWaypoint.transform.position;
	}

	// Update is called once per frame
	protected override void Update () {
		velocity = rb.velocity;
		posNextWayPoint_Relative = pos_current_wayPoint - transform.position;

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

		Debug.DrawRay (transform.position, rb.velocity, Color.red);
		Debug.DrawRay (transform.position, adjust_Direction (velocity, posNextWayPoint_Relative) , Color.green);
		Debug.DrawRay (transform.position, posNextWayPoint_Relative, Color.white);

		if ((transform.position - pos_current_wayPoint).magnitude <= 2) {
			//SetNextWayPoint();

			//Este deberá de ser el de la collección de waypoint del levelManager del story
			//si ese array esta lleno la direccion hacia el del numero 0
			//si esta vacio sigo con la velocidad que llevaba y direccion
			//level_manager.AdvanceWaypoint();
		//	GameObject nextWaypoint = level_manager.CurrentWaypoint;

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
