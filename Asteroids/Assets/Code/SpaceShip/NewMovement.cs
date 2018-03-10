using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour {

	public Transform nextWayPoint;
	public float updateTime;
	public float force;

	private Rigidbody rb;
	private bool second_Movement = false;
	private bool first_Movement = false;
	private float currentUpdateTime;

    private Vector3 adjustedDirection; // Here for testing

    // Use this for initialization
    void Start () {
		
		rb = gameObject.GetComponent<Rigidbody> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity = rb.velocity;
		Vector3 posNextWayPoint_Relative = nextWayPoint.position - transform.position;

		currentUpdateTime += Time.deltaTime;

		float Angle_resultant = Vector3.Angle (velocity, posNextWayPoint_Relative);

		//Debug.Log (Angle_resultant);

		if (!first_Movement) {
			rb.AddForce (posNextWayPoint_Relative.normalized * force, ForceMode.Impulse);
			first_Movement = true;
		} 
		else if(second_Movement && currentUpdateTime > updateTime)
		{
            adjustedDirection = adjust_Direction(velocity, posNextWayPoint_Relative).normalized;
            rb.AddForce (adjustedDirection * force, ForceMode.Impulse);
            // TODO: Guardar un valor de orientación
            // Y hacer una transición más limpia con Slerp
            transform.LookAt(adjustedDirection);
			currentUpdateTime = 0;
		}
			
		Debug.DrawRay (transform.position, rb.velocity, Color.red);
		Debug.DrawRay (transform.position, adjust_Direction (velocity, posNextWayPoint_Relative) , Color.green);
		Debug.DrawRay (transform.position, posNextWayPoint_Relative, Color.white);

		if ((transform.position - nextWayPoint.position).magnitude <= 2) {
			//cojo el siguiente wayPoint
			SetNextWayPoint();

			first_Movement = true;
			second_Movement = true;
		}
	}

	Vector3 adjust_Direction(Vector3 pos, Vector3 obj)
	{
		return (obj - pos);
	}

	void SetNextWayPoint()
	{
		nextWayPoint = nextWayPoint.GetComponent<WayPoint>().nextWayPoint;
	}
}
