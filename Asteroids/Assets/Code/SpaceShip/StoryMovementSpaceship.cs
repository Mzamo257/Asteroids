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
        posCurrentWayPoint = Vector3.zero; /*level_manager.CurrentWaypoint.transform.position;*/
		counterWaypoints = 0;
	}

	// Update is called once per frame
	protected override void Update () {
		
		velocity = rb.velocity;
		posNextWayPointRelative = posCurrentWayPoint - transform.position;

		currentUpdateTime += Time.deltaTime;

		//generateWaypoints (currentUpdateTime);

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
		
			previousRotation = nextRotation;
			nextRotation = Quaternion.LookRotation (posNextWayPointRelative);
			currentUpdateTime = 0;
		}
			
		transform.rotation = Quaternion.Slerp (previousRotation, nextRotation, currentUpdateTime);

		Debug.DrawRay (transform.position, rb.velocity, Color.red);
		Debug.DrawRay (transform.position, adjustDirection (velocity, posNextWayPointRelative) , Color.green);
		Debug.DrawRay (transform.position, posNextWayPointRelative, Color.white);

		if ((transform.position - posCurrentWayPoint).magnitude <= 2) {

			//Este deberá de ser el de la collección de waypoint del levelManager del story
			//si ese array esta lleno la direccion hacia el del numero 0
			//si esta vacio sigo con la velocidad que llevaba y direccion
			levelManager.ReachWaypoint();

			//tengo que coger el siguiente waypoint 
			GameObject nextWaypoint = levelManager.CurrentWaypoint;
			/*

			if (nextWaypoint != null)
			{
				posCurrentWayPoint = nextWaypoint.transform.position;
			}*/

			firstMovement = true;
			secondMovement = true;
		}
	}

	#endregion


	#region User Methods
	private void generateWaypoints(float counter)
	{
		//cambiar este counter sino creare waypoint cada dos por tres
		if (counter < 2) {
			levelManager.SetWaypoint( transform.position + new Vector3(0,0,100) );
			Debug.Log ("He creado un waypoint");
			//currentUpdateTime = 0;
		}
	}
	#endregion
}
