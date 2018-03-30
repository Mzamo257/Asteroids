﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HUD : MonoBehaviour {
	#region Public Attributes
	public Texture[] alertBars;
	public Texture[] progress;
	public float iconSizeRate = 0.2f;
	public float damage;
	#endregion

	#region Private Attributes
	protected float iconSize;
	protected GameObject gameManager;
	protected GameManager gmScript;
	#endregion


	#region MonoDevelop Methods
	// Use this for initialization
	protected virtual void Start () {
		gameManager = GameObject.Find ("GameManager");
		gmScript = gameManager.GetComponent<GameManager> ();

		iconSize = Screen.height * iconSizeRate;

		damage = gmScript.GetSpaceshipLife();
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (damage > gmScript.GetSpaceshipLife())	damage -= 10 * Time.deltaTime;
	
	}

	protected virtual void OnGUI()
	{
		//Healthbar
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize / 6, damage / 100 * (iconSize * 2.9f), iconSize / 3), alertBars [3], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize / 6, gmScript.GetSpaceshipLife () / 100 * (iconSize * 2.9f), iconSize / 3), alertBars [2], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, 13, iconSize * 3, iconSize / 2), alertBars [1], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize, 0, iconSize/1.5f, iconSize/1.5f), alertBars [0], ScaleMode.ScaleToFit);
	}
	#endregion

	#region User Methods
	#endregion
}
