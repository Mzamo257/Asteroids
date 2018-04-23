using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HUD : MonoBehaviour {
	#region Public Attributes
	public Texture[] alertBars;
	public float iconSizeRate = 0.2f;
	public float damage;
	#endregion

	#region Private Attributes
	protected float iconSize;
	protected GameObject gameManager;
	// protected GameManager gmScript;
    protected GameManagerSingleton gmSingScript;
	protected BaseLevelManager levelMgr;
	public Texture asteroidPointer;
	#endregion


	#region MonoDevelop Methods
	// Use this for initialization
	protected virtual void Start () {
		// gmScript = FindObjectOfType<GameManager>();
        // gmSingScript = GameManagerSingleton.GetInstance();

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
		//Healthbar
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize / 6, damage / levelMgr.SpaceMaxLife * (iconSize * 2.9f), iconSize / 3), alertBars [3], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize / 6, levelMgr.SpaceCurrentLife / levelMgr.SpaceMaxLife * (iconSize * 2.9f), iconSize / 3), alertBars [2], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, 13, iconSize * 3, iconSize / 2), alertBars [1], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize, 0, iconSize/1.5f, iconSize/1.5f), alertBars [0], ScaleMode.ScaleToFit);

		if (levelMgr.AsteroidSelected) 
		{
			GUI.DrawTexture(new Rect(levelMgr.AsteroidPosCamera.x, levelMgr.AsteroidPosCamera.y, iconSize /1.5f, iconSize/1.5f), asteroidPointer, ScaleMode.ScaleToFit);
		}
	}
	#endregion

	#region User Methods
	#endregion
}
