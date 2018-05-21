using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDstory : HUD {
	
	#region Public Attributes
	public RawImage minimap;
	public Texture hook;
	public Texture alien;
	public GameObject bigMap;
	#endregion
	#region Private Attributes
	private RectTransform minimapDimension;
	private Rect uvRect;
	private StoryLevelManager storyLevelMgr;
	#endregion

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		minimapDimension = minimap.rectTransform;
		storyLevelMgr = levelMgr as StoryLevelManager;

		bigMap.transform.localScale = new Vector3 (0.004f * Screen.width/4, 0.005f * Screen.height/4, 1);
		bigMap.transform.position = new Vector3 (Screen.width *5/ 10, Screen.height * 4.5f / 10, 1);
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
				//level
				GUI.Label (new Rect (iconSize * 2, iconSize / 15, 100, 30), "Level " + GameManagerSingleton.instance.CurrentStoryLevel, levelStyle);
				//Hooks
				GUI.DrawTexture (new Rect (Screen.width * 6.3f / 10, Screen.height * 0.3f / 10, Screen.width *1.7f/ 10, Screen.height*0.8f/ 10), hook, ScaleMode.StretchToFill);
				GUI.Label (new Rect (Screen.width * 7.2f / 10, Screen.height *1.1f/ 20, Screen.width *0.3f/ 10, Screen.height*0.3f / 10), storyLevelMgr.AvailableTrash + "/" + storyLevelMgr.TotalTrash, levelStyle);
				//Aliens
				GUI.DrawTexture (new Rect (Screen.width * 8.1f / 10, Screen.height * 0.3f / 10, Screen.width  *1.5f/ 10, Screen.height *0.8f/ 10), alien, ScaleMode.StretchToFill);
				GUI.Label (new Rect (Screen.width * 8.9f / 10, Screen.height *1.1f/ 20, Screen.width *0.3f/ 10, Screen.height *0.3f/ 10), storyLevelMgr.CaughtAliens + "/" + storyLevelMgr.AliensToCatch, levelStyle);

			}
		}
	}

	public Vector2 checkClick()
	{
		Vector2 positionVector = new Vector2 (-1f, -1f);
		if (Input.GetMouseButtonUp(0)) {
			Vector3 mPosition = Input.mousePosition;
			if (mPosition.x >= minimapDimension.position.x && mPosition.y >= minimapDimension.position.y
			    && mPosition.x <= minimapDimension.position.x + minimapDimension.rect.size.x * minimapDimension.localScale.x
			    && mPosition.y <= minimapDimension.position.y + minimapDimension.rect.size.y * minimapDimension.localScale.y) 
			{
				positionVector.x = (mPosition.x - minimapDimension.position.x) / (minimapDimension.rect.size.x * minimapDimension.localScale.x);
				positionVector.y = (mPosition.y - minimapDimension.position.y) / (minimapDimension.localScale.y * minimapDimension.rect.size.y);
			}
		}
		return positionVector;
	}
}
