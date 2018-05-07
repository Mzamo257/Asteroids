using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HUD : MonoBehaviour {
	#region Public Attributes
	public Texture[] alertBars;
	public float iconSizeRate = 0.2f;
	public float damage;
	public Canvas Lose;
	public Canvas Win;
	public Canvas Pause;
	public GUIStyle levelStyle;
	#endregion

	#region Private Attributes
	protected float iconSize;
	protected GameObject gameManager;
    protected GameManagerSingleton gmSingScript;
	protected BaseLevelManager levelMgr;
	public Texture asteroidPointer;
	#endregion


	#region MonoDevelop Methods
	// Use this for initialization
	protected virtual void Start () {

        levelMgr = FindObjectOfType<BaseLevelManager> ();

		iconSize = Screen.height * iconSizeRate;

		damage = levelMgr.SpaceCurrentLife;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (damage > levelMgr.SpaceCurrentLife)	damage -= 10 * Time.deltaTime;	
	}

	protected virtual void OnGUI()
	{
		if (levelMgr.currentState == GameState.Victory) {
			Win.enabled = true;
			Pause.enabled = false;
		} else if (levelMgr.currentState == GameState.Defeat) {
			Lose.enabled = true;
			Pause.enabled = false;
		} else if (levelMgr.currentState != GameState.Paused){
			//Healthbar
			GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize *2/ 6, damage / levelMgr.SpaceMaxLife * (iconSize * 2.5f), iconSize / 6), alertBars [3], ScaleMode.StretchToFill);
			GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize *2/ 6, levelMgr.SpaceCurrentLife / levelMgr.SpaceMaxLife * (iconSize * 2.5f), iconSize / 6), alertBars [2], ScaleMode.StretchToFill);
			GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize *1.75f/ 6, iconSize * 2.6f, iconSize *1.2f / 5), alertBars [1], ScaleMode.StretchToFill);
			GUI.DrawTexture (new Rect (iconSize, iconSize*1.3f/6, iconSize / 1.2f, iconSize / 2.5f), alertBars [0], ScaleMode.ScaleToFit);


			if (levelMgr.AsteroidSelected) {
				GUI.DrawTexture (new Rect (levelMgr.AsteroidPosCamera.x, levelMgr.AsteroidPosCamera.y, iconSize / 1.5f, iconSize / 1.5f), asteroidPointer, ScaleMode.ScaleToFit);
			}
		}
	}
	#endregion

	#region User Methods
	#endregion
}
