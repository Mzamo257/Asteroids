using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {
	#region Public Attributes
	public Texture[] alertBars;
	public Texture[] progress;
	public float iconSizeRate = 0.2f;
	public float damage;
	#endregion

	#region Private Attributes
	private float iconSize;
	private GameObject gameManager;
	private GameManager gmScript;
	#endregion


	#region MonoDevelop Methods
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
		//Healthbar
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize / 6, damage / 100 * (iconSize * 2.9f), iconSize / 3), alertBars [3], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, iconSize / 6, gmScript.GetSpaceshipLife () / 100 * (iconSize * 2.9f), iconSize / 3), alertBars [2], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize + iconSize / 2, 13, iconSize * 3, iconSize / 2), alertBars [1], ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (iconSize, 0, iconSize/1.5f, iconSize/1.5f), alertBars [0], ScaleMode.ScaleToFit);

		//Spaceship progress
		GUI.DrawTexture(new Rect(Screen.width*9/10, Screen.height * 2 / 10, iconSize/ 10, iconSize * 3), progress[2], ScaleMode.StretchToFill);
		//Cambiar 4 por número de waypoints
		float positionY = iconSize * 3 / 4;
		//Cambiar 4 por número de waypoints
		//Cambiar 0 por número de waypoints pasados
		for (int i = 0; i < 4; i++) 
		{
			GUI.DrawTexture(new Rect(Screen.width*8.7f/10, Screen.height*2/10 + positionY * i, iconSize/2, iconSize/3), progress[0], ScaleMode.ScaleToFit);
		}
		//Cambiar 4 por número de waypoints
		//Cambiar 3 por número de waypoints pasados - numero total
		for (int i = 3; i < 4; i++) 
		{
			GUI.DrawTexture(new Rect(Screen.width*8.7f/10, Screen.height*2/10 + positionY * i, iconSize/2, iconSize/3), progress[1], ScaleMode.ScaleToFit);
		}

		GUI.DrawTexture (new Rect (Screen.width * 8.8f/10, Screen.height * 2 / 10 + iconSize * 3, iconSize / 2.8f, iconSize / 2.8f), progress [0], ScaleMode.ScaleToFit);
	}
	#endregion

	#region User Methods
	#endregion
}
