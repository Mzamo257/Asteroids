using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO DO: Cambiar el nombre de la clase
public class level_Manager : MonoBehaviour {

	public GameObject wayPoint_prefab;

	private List<GameObject> list_of_wayPoints;
	private GameManager gameManager;
	private data_Level level_data;
	private float spaceshipCurrentLife;

	private float maxSpaceshipLife = 1000;

	#region Properties
	public int NumWaypoints {
		get{ return list_of_wayPoints.Count; }
	}
	public float SpaceCurrentLife {
		get { return spaceshipCurrentLife; }
	}

	public float SpaceMaxLife {
		get { return maxSpaceshipLife;}
	}
	#endregion

	// Use this for initialization
	void Start () 
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		level_data = gameManager.current_Level_data;
		spaceshipCurrentLife = maxSpaceshipLife;
        // Debug.Log("Number of waypoints: " + level_data.numberOf_Waypoints);
		//create waypoints
		list_of_wayPoints = new List<GameObject> ();
		for(int i = 0 ; i < level_data.numberOf_Waypoints; i++)
		{
			Vector3 wayPoint_position = new Vector3(0,0, 100*i);
			GameObject new_WayPoint = Instantiate(wayPoint_prefab , wayPoint_position, Quaternion.identity);
			list_of_wayPoints.Add (new_WayPoint);
		}
        // Debug.Log("Waypoint list length: " + list_of_wayPoints.Count);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject getWaypoint(int position)
	{
		if (position < list_of_wayPoints.Count)
			return list_of_wayPoints [position];
		else
			return null;
	}
}
