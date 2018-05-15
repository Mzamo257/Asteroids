using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDsurvival : HUD {

	public GUIStyle textStyle;

    private SurvivalLevelManager survivalLevelMgr;
	private float totalDistance;
	private float finalPosition;
	private float startPosition;
	private float positionY;
	public Texture[] progress;
	public Texture ship;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
        survivalLevelMgr = levelMgr as SurvivalLevelManager;
		positionY = iconSize * 3 / survivalLevelMgr.NumWaypoints;

		totalDistance = survivalLevelMgr.calculateDistanceNextWaypoint();
		finalPosition = Screen.height * 2 / 10;
		startPosition = positionY * survivalLevelMgr.NumWaypoints + finalPosition;
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void OnGUI()
	{
		if (levelMgr.currentState == GameState.WaitingToStart) {
			Pause.enabled = false;
		} else {
			base.OnGUI ();
			if (levelMgr.currentState == GameState.InGame) {
				//Current level
				GUI.Label (new Rect (iconSize * 2, iconSize / 15, 100, 30), "Level " + GameManagerSingleton.GetInstance ().CurrentSurvivalLevel, levelStyle);
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

	private float getProgress()
	{
		if (survivalLevelMgr.CurrentWaypointIndex < survivalLevelMgr.NumWaypoints) {
			
			int point = (survivalLevelMgr.NumWaypoints - survivalLevelMgr.CurrentWaypointIndex) - 1;
			float v = survivalLevelMgr.calculateDistanceNextWaypoint () / totalDistance;
	
			return Mathf.Lerp (finalPosition + positionY * point, finalPosition + positionY * (point + 1), v);
		} else
			return finalPosition;
	}

	public void updateParameters()
	{
		totalDistance = survivalLevelMgr.calculateDistanceNextWaypoint ();
	}
}
