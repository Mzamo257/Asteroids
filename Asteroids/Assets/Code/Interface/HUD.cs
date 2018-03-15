using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

	public Texture[] alertBars;
	public float iconSizeRate = 0.2f;

	private float iconSize;
	private GameObject gameManager;
	private GameManager gmScript;

	public float damage;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		gmScript = gameManager.GetComponent<GameManager> ();

		iconSize = Screen.height * iconSizeRate;

		damage = gmScript.GetSpaceshipLife();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (damage > gmScript.GetSpaceshipLife())	damage -= 10 * Time.deltaTime;
	
	}

	void OnGUI()
	{
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize / 6, damage / 100 * (iconSize * 3), iconSize / 3), alertBars [3], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize / 6, gmScript.GetSpaceshipLife () / 100 * (iconSize * 2.9f), iconSize / 3), alertBars [2], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, 13, iconSize * 3, iconSize / 2), alertBars [1], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize, 0, iconSize/1.5f, iconSize/1.5f), alertBars [0], ScaleMode.ScaleToFit);
	}
}
