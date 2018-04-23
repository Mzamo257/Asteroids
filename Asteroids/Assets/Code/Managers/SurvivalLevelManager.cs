using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalLevelManager : BaseLevelManager {
	public HUDsurvival surHud;

    #region Private Attributes
    protected SurvivalLevelData levelData;
    protected List<GameObject> list_of_wayPoints;
    protected int currentWaypoint; 
	protected SurvivalIntro intro;
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
		get {  if (currentWaypoint < NumWaypoints)
				return list_of_wayPoints [currentWaypoint];
			else
				return null;}
    }
    #endregion

    #region MonoBehaviour Methods
    // Use this for initialization
    protected override void Start () {
        base.Start();
		intro = GetComponent<SurvivalIntro>();
		intro.StartEvent ();
        //
        gameManagerSingleton.CurrentGameMode = GameMode.Survival;
        levelData = gameManagerSingleton.CurrentSurvivalLevelData;
        
        // Debug.Log("Number of waypoints: " + level_data.numberOf_Waypoints);
        //create waypoints
        list_of_wayPoints = new List<GameObject>();
        for (int i = 0; i < levelData.numberOfWaypoints; i++)
        {
            //
            float x = Random.Range(-5.0f, 5.0f);
            float y = Random.Range(-5.0f, 5.0f);
            //
            Vector3 wayPoint_position = new Vector3(x, y, 100 * i);
            GameObject new_WayPoint = Instantiate(wayPoint_prefab, wayPoint_position, Quaternion.identity);
            list_of_wayPoints.Add(new_WayPoint);
        }
        // Debug.Log("Waypoint list length: " + list_of_wayPoints.Count);
        //
        surHud = FindObjectOfType<HUDsurvival>();
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
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
        // Debug.Log("Current waypoint: " + currentWaypoint + ", num of waypoints: " + list_of_wayPoints.Count);
        if(currentWaypoint > list_of_wayPoints.Count)
        {
            Debug.Log("Defeat");
            GameManagerSingleton.GetInstance().SurvivalProgress();
			lose = true;
            gameState = GameState.Defeat;
            Time.timeScale = 0.0f;
			//PauseGame ();
        }
		else
		surHud.updateParameters();
    }

    public float calculateDistanceNextWaypoint()
    {
        return (list_of_wayPoints[currentWaypoint].transform.position - ship.transform.position).magnitude;
    }

	public override bool introEnd()
	{
		if (intro.Step == 1)
			return true;
		return false;
	}

    #endregion

}
