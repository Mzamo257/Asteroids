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
	public Texture back;
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
		
			Pause.enabled = true;
			if (levelMgr.currentState == GameState.Victory) {
				Win.enabled = true;
				Pause.enabled = false;
			} else if (levelMgr.currentState == GameState.Defeat) {
				Lose.enabled = true;
				Pause.enabled = false;
			} else if (levelMgr.currentState != GameState.Paused) {
				//Base
				//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height * 1.5f/10), back, ScaleMode.StretchToFill);
				//Healthbar
			GUI.DrawTexture (new Rect (Screen.width*2.3f/10, Screen.height*0.6f/10, damage / levelMgr.SpaceMaxLife * (Screen.width*3.8f/10), Screen.height*0.5f/10), alertBars [2], ScaleMode.StretchToFill);
				GUI.DrawTexture (new Rect (Screen.width*2f/10, Screen.height*0.6f/10, levelMgr.SpaceCurrentLife / levelMgr.SpaceMaxLife * (Screen.width*4/10),Screen.height*0.5f/10), alertBars [1], ScaleMode.StretchToFill);
				GUI.DrawTexture (new Rect (Screen.width*2f/10, Screen.height*0.6f/10, Screen.width*4/10, Screen.height*0.5f/10), alertBars [0], ScaleMode.StretchToFill);

				if (levelMgr.AsteroidSelected) {
					GUI.DrawTexture (new Rect (levelMgr.AsteroidPosCamera.x - asteroidPointer.width * 0.04f, levelMgr.AsteroidPosCamera.y - asteroidPointer.height * 0.02f, iconSize / 1.5f, iconSize / 1.5f), asteroidPointer, ScaleMode.ScaleToFit);
				}
			}
	}
	#endregion

	#region User Methods
	#endregion
}
