using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

	public Texture[] alertBars;
	public float iconSizeRate = 0.2f;


	private float iconSize;
	private GameObject gameManager;
	private GameManager gmScript;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		gmScript = gameManager.GetComponent<GameManager> ();

		iconSize = Screen.height * iconSizeRate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize / 8, gmScript.GetAlertLevel () / 100 * (iconSize * 4), iconSize / 4), alertBars [2], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, 0, iconSize * 4, iconSize / 2), alertBars [1], ScaleMode.ScaleToFit);
		GUI.DrawTexture (new Rect (iconSize, 0, iconSize/1.5f, iconSize/1.5f), alertBars [0], ScaleMode.ScaleToFit);
	}
}
