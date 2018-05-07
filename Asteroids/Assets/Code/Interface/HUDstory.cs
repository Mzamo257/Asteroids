using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDstory : HUD {
	
	#region Public Attributes
	public RawImage minimap;
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
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void OnGUI()
	{
		base.OnGUI ();
		if (!levelMgr.Pause) {
			GUI.Label (new Rect (iconSize * 2, iconSize/15, 100, 30), "Level " + GameManagerSingleton.GetInstance ().CurrentStoryLevel, levelStyle);
			GUI.Label (new Rect (Screen.width * 8.7f / 10, Screen.height / 20, Screen.width / 10, Screen.height / 10), storyLevelMgr.CaughtAliens + " / " + storyLevelMgr.AliensToCatch, levelStyle);
			GUI.Label (new Rect (Screen.width * 7.2f / 10, Screen.height / 20, Screen.width / 10, Screen.height / 10), storyLevelMgr.AvailableTrash + " / " + storyLevelMgr.TotalTrash, levelStyle);
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
