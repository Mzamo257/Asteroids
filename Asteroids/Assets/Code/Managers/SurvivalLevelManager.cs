using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalLevelManager : BaseLevelManager {

    #region Public Attributes

    public HUDsurvival surHud;

    #endregion

    #region Private Attributes

    protected SurvivalLevelData levelData;
    protected List<GameObject> waypointList;
    protected int currentWaypoint; 
	protected SurvivalIntro intro;

    #endregion

    #region Properties

    public int NumWaypoints
    {
        get { return waypointList.Count; }
    }

    public int CurrentWaypointIndex
    {
        get { return currentWaypoint; }
    }

    public GameObject CurrentWaypoint
    {
		get {
            if (currentWaypoint < NumWaypoints)
				return waypointList [currentWaypoint];
			else
				return null;
        }
    }
    #endregion

    #region MonoBehaviour Methods
    // Use this for initialization
    protected override void Start () {
        // And then the base start
        base.Start();

        intro = GetComponent<SurvivalIntro>();
		intro.StartEvent ();

        // Get the game manager and the level data
        gameManagerSingleton.CurrentGameMode = GameMode.Survival;
        levelData = gameManagerSingleton.CurrentSurvivalLevelData;
        
        // Create waypoints
        waypointList = new List<GameObject>();
        for (int i = 0; i < levelData.numberOfWaypoints; i++)
        {
            //
            float x = Random.Range(-5.0f, 5.0f);
            float y = Random.Range(-5.0f, 5.0f);
            //
            Vector3 wayPoint_position = new Vector3(x, y, 100 * i);
            GameObject new_WayPoint = Instantiate(wayPoint_prefab, wayPoint_position, Quaternion.identity);
            waypointList.Add(new_WayPoint);
        }
        //
        surHud = FindObjectOfType<HUDsurvival>();

        // Assign the rest of the data
        // NOTE: This should be in BaseLevelManager, have to find the way
        asteroidManager.numAsteroids = levelData.numberOfAsteroids;
        ship.force = levelData.spaceshipForce;
        ship.maxSpeed = levelData.spaceshipMaxSpeed;
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Give the current waypoint according to the index
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject getWaypoint(int position)
    {
        if (position < waypointList.Count)
            return waypointList[position];
        else
            return null;
    }

    /// <summary>
    /// Adavance the waypoint index
    /// In order to reorient the ship and update the HUD info
    /// Or finish the level when reached the last one
    /// </summary>
    public void AdvanceWaypoint()
    {
        if (currentWaypoint < waypointList.Count - 1)
        {
            currentWaypoint++;
            //Tell the hud that the waypoint has change
            surHud.updateParameters();
        }
        else
        {
            gameState = GameState.Defeat;
        }
		
    }

    /// <summary>
    /// Calculate the distance to the next waypoint
    /// In order to show the progress in the HUD
    /// </summary>
    /// <returns></returns>
    public float calculateDistanceNextWaypoint()
    {
        return (waypointList[currentWaypoint].transform.position - ship.transform.position).magnitude;
    }

    /// <summary>
    /// Give the score depending the remaining waypoints/hooks
    /// </summary>
    /// <returns></returns>
    protected override int GetScoreFromWaypoints()
    {
        // Podríamos contar también la distancia que había al siguiente waypoint
        
        float shipProgress = 1 - (float)currentWaypoint / (float)NumWaypoints;
        float pointsToScore = 100 * shipProgress;
        Debug.Log("Current waypoint: " + currentWaypoint + ", num waypoints: " + NumWaypoints + 
            ", ship progress: " + shipProgress + ", points to score: " + pointsToScore);
        return (int)pointsToScore;
    }

    #endregion

}
