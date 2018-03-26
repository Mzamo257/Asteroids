using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_Manager : MonoBehaviour {

	public GameObject wayPoint_prefab;

	private List<GameObject> list_of_wayPoints;
	private GameManager gameManager;
	private data_Level level_data;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		level_data = gameManager.current_Level_data;

		//create waypoints
		list_of_wayPoints = new List<GameObject> ();
		for(int i = 0 ; i < level_data.numberOf_Waypoints; i++)
		{
			Vector3 wayPoint_position = new Vector3(0,0, 100*i);
			GameObject new_WayPoint = Instantiate(wayPoint_prefab , wayPoint_position, Quaternion.identity);
			list_of_wayPoints.Add (new_WayPoint);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject getWaypoint(int position)
	{
		return list_of_wayPoints [position];
	}
}
