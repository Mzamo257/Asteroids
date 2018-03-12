using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public float maxDist = 1.0f;
	public Vector3 camOffset = new Vector3(0.15f, 0.8f, 1.2f);
	public Vector3 targetOffset = new Vector3(0, 0.4f, -1.0f);
	public float dampingK = 4.0f;
	public float springK = 8.0f;
	public Transform targetPlayer;

	private Vector3 vel = new Vector3(0, 0, 0);

	void Start()
	{
		Vector3 idealPos = targetPlayer.TransformPoint(camOffset);
		transform.position = idealPos;
		Vector3 targetPos = targetPlayer.TransformPoint(targetOffset);
        //transform.LookAt(targetPos, Vector3.up);
        Quaternion objectiveRotation = Quaternion.LookRotation(targetPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, objectiveRotation, 0.2f);
	}

	void Update()
	{
		float dt = Time.deltaTime;

		Vector3 idealPos = targetPlayer.TransformPoint(camOffset);
		Vector3 offsetPos = idealPos - transform.position;
		Vector3 springForce = springK * offsetPos;
		Vector3 dampingForce = -vel * dampingK;
		Vector3 accel = springForce + dampingForce;
		vel += accel * dt;
		transform.position += vel * dt;
		offsetPos = idealPos - transform.position;
		offsetPos = Vector3.ClampMagnitude(offsetPos, maxDist);
		transform.position = idealPos - offsetPos;
		Vector3 targetPos = targetPlayer.TransformPoint(targetOffset);
		transform.LookAt(targetPos, Vector3.up);

	}
}
