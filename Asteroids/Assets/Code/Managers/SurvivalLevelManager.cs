using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalLevelManager : BaseLevelManager {

    #region Private Attributes
    protected SurvivalLevelData levelData;
    protected List<GameObject> list_of_wayPoints;
    protected int currentWaypoint;
    #endregion

    #region Properties
    public int NumWaypoints
    {
        get { return list_of_wayPoints.Count; }
    }

    public int CurrentWaypointIndex
    {
        get { return currentWaypoint; }
    }

    public GameObject CurrentWaypoint
    {
        get { return list_of_wayPoints[currentWaypoint]; }
    }
    #endregion

    #region MonoBehaviour Methods
    // Use this for initialization
    protected override void Start () {
        base.Start();
        //
        gameManager.CurrentGameMode = GameMode.Survival;
        level_data = gameManager.CurrentLevelData;
        // Debug.Log("Number of waypoints: " + level_data.numberOf_Waypoints);
        //create waypoints
        list_of_wayPoints = new List<GameObject>();
        for (int i = 0; i < level_data.numberOf_Waypoints; i++)
        {
            Vector3 wayPoint_position = new Vector3(0, 0, 100 * i);
            GameObject new_WayPoint = Instantiate(wayPoint_prefab, wayPoint_position, Quaternion.identity);
            list_of_wayPoints.Add(new_WayPoint);
        }
        // Debug.Log("Waypoint list length: " + list_of_wayPoints.Count);
    }

    // Update is called once per frame
    protected override void Update () {
		
	}

    #endregion

    #region Methods

    public GameObject getWaypoint(int position)
    {
        if (position < list_of_wayPoints.Count)
            return list_of_wayPoints[position];
        else
            return null;
    }

    public void AdvanceWaypoint()
    {
        currentWaypoint++;
        if(currentWaypoint > list_of_wayPoints.Count)
        {
            Debug.Log("Defeat");
            SceneManager.LoadScene("Menu");
        }
    }

<<<<<<< HEAD
    #endregion
=======
	public float calculateDistance()
	{
		float distance = 0;
		for (int i = NumWaypoints-1; i > currentWaypoint; i--) 
		{
			distance += (list_of_wayPoints [i].transform.position - list_of_wayPoints [i-1].transform.position).magnitude;;
		}

		distance += (list_of_wayPoints [currentWaypoint].transform.position - ship.transform.position).magnitude;
		return distance;
	}
>>>>>>> aea0a0b1e38e855bc00bdcd24c9ab882ff277e33
}
