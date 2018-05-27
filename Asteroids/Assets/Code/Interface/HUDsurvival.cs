using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDsurvival : HUD {
    #region Public Attributes
    public Texture[] progress;
    public Texture ship;
    public GUIStyle textStyle;
    #endregion

    #region Private Attributes
    private SurvivalLevelManager survivalLevelMgr;
	private float totalDistance;
	private float finalPosition;
	private float positionY;
    #endregion

    #region MonoDEvelop Methods
    // Use this for initialization
    protected override void Start ()
    {
		base.Start ();
        survivalLevelMgr = levelMgr as SurvivalLevelManager;
		positionY = iconSize * 3 / survivalLevelMgr.NumWaypoints;

		totalDistance = survivalLevelMgr.calculateDistanceNextWaypoint();
		finalPosition = Screen.height * 2 / 10;
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void OnGUI()
	{
		if (levelMgr.currentState == GameState.WaitingToStart)
        {
			Pause.enabled = false;
		}
        else
        {
			base.OnGUI ();
			if (levelMgr.currentState == GameState.InGame)
            {
				//Current level
				GUI.Label (new Rect (iconSize * 2, iconSize / 15, 100, 30), levelText + " " + GameManagerSingleton.instance.CurrentSurvivalLevel, levelStyle);
				//Spaceship progress
				//Line
				GUI.DrawTexture (new Rect (Screen.width * 9 / 10, finalPosition, iconSize / 10, iconSize * 3), progress [2], ScaleMode.StretchToFill);
				//Waypoints
				for (int i = 0; i < survivalLevelMgr.NumWaypoints - survivalLevelMgr.CurrentWaypointIndex; i++) {
					GUI.DrawTexture (new Rect (Screen.width * 8.7f / 10, finalPosition + positionY * i, iconSize / 2, iconSize / 3), progress [0], ScaleMode.ScaleToFit);
				}
				//Waypoints pass
				for (int i = survivalLevelMgr.NumWaypoints - survivalLevelMgr.CurrentWaypointIndex; i < survivalLevelMgr.NumWaypoints; i++) {
					GUI.DrawTexture (new Rect (Screen.width * 8.7f / 10, finalPosition + positionY * i, iconSize / 2, iconSize / 3), progress [1], ScaleMode.ScaleToFit);
				}
				//Spaceship
				GUI.DrawTexture (new Rect (Screen.width * 8.8f / 10, getProgress (), iconSize / 2.8f, iconSize / 2.8f), ship, ScaleMode.ScaleToFit);
			}
		}
	}
    #endregion

    #region User Methods
    /// <summary>
    /// Returns the current position of the ship image in the progress bar
    /// </summary>
    /// <returns>Position in the screen</returns>
    private float getProgress()
	{
        //check if it is the last waypoint
        if (survivalLevelMgr.CurrentWaypointIndex < survivalLevelMgr.NumWaypoints)
        {
            //get the correct number of the next waypoint
            int point = (survivalLevelMgr.NumWaypoints - survivalLevelMgr.CurrentWaypointIndex) - 1;
            //calculate the position regarding the distance to the next waypoint
            float interval = survivalLevelMgr.calculateDistanceNextWaypoint() / totalDistance;
            //return the correct position between two waypoints
            return Mathf.Lerp(finalPosition + positionY * point, finalPosition + positionY * (point + 1), interval);
        }
        else
        {
            return finalPosition;
        }
	}

    /// <summary>
    /// Get the current distance between the two waypoints where the ship is
    /// </summary>
	public void updateParameters()
	{
		totalDistance = survivalLevelMgr.calculateDistanceNextWaypoint ();
	}
    #endregion
}
