using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalMovementSpaceship : BaseSpaceship {

    #region Public Attribute
    public float barrelRollSpeed = 180.0f;
	#endregion

	#region Private Attributes
	private SurvivalLevelManager levelManager;
	private float angleResultant;
	private float maxDetectionDistance = 10.0f;
	private RaycastHit hitInfo;
    //
    private int barrelRollDirection = 0;
    private float barrelRollProgression = 0;
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
		
        if(barrelRollDirection == 0)
        {
            Dodge();
        }

		velocity = rb.velocity;
		currentUpdateTime += Time.deltaTime;
		angleResultant = Vector3.Angle (velocity, posNextWayPointRelative);
		rb.velocity = Vector3.ClampMagnitude (rb.velocity , maxSpeed);

		posNextWayPointRelative = posCurrentWayPoint - transform.position;

		Movement ();

		base.VerticalSpeedControl ();

		NextWaypoint ();

	}
	#endregion

	#region User Methods
	protected override void Movement()
	{
		if(currentUpdateTime > updateTime)
		{
			adjustedDirection = AdjustDirection(velocity, posNextWayPointRelative).normalized;
			rb.AddForce (adjustedDirection * force, ForceMode.Impulse);

			previousRotation = nextRotation;
			nextRotation = Quaternion.LookRotation (posNextWayPointRelative);
			currentUpdateTime = 0;
		}
		transform.rotation = Quaternion.Slerp (previousRotation, nextRotation, currentUpdateTime);
       
        //Check if it is rotating
        if(Mathf.Abs(barrelRollProgression) < 360)
        {
            float rotationToApply = barrelRollDirection * barrelRollSpeed * Time.deltaTime;

            shipModel.Rotate(transform.forward * rotationToApply);
            barrelRollProgression += rotationToApply;
        }
        else
        {
            barrelRollDirection = 0;
            shipModel.eulerAngles = new Vector3 (0,0,0);
        }
	}

	protected override void Dodge()
	{
		if (Physics.Raycast (transform.position, rb.velocity, out hitInfo, maxDetectionDistance)) {

			if(hitInfo.transform.tag.Equals("Asteroid"))
			{
				int random = UnityEngine.Random.Range (0,2);
                if (random == 0) {
					rb.AddForce (transform.right*20, ForceMode.Impulse);
                    barrelRollDirection = 1;
                    barrelRollProgression = 0;
				} else if(random == 1){
					rb.AddForce (-transform.right*20, ForceMode.Impulse);
                    barrelRollDirection = -1;
                    barrelRollProgression = 0;
				}
			}
		}	
	}

	protected override void NextWaypoint()
	{

		if ((transform.position - posCurrentWayPoint).magnitude <= 2) {

			levelManager.AdvanceWaypoint();
			GameObject nextWaypoint = levelManager.CurrentWaypoint;

			if (nextWaypoint != null)
			{
				posCurrentWayPoint = nextWaypoint.transform.position;
			}
				
		}
	}
	#endregion
}
