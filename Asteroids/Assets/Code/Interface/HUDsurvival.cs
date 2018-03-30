using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDsurvival : HUD {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void OnGUI()
	{
		base.OnGUI ();
		//Spaceship progress
		//Line
		GUI.DrawTexture (new Rect (Screen.width * 9 / 10, Screen.height * 2 / 10, iconSize / 10, iconSize * 3), progress [2], ScaleMode.StretchToFill);
		//Cambiar 4 por número de waypoints
		float positionY = iconSize * 3 / 4;
		//Waypoints
		//Cambiar 4 por número de waypoints
		//Cambiar 0 por número de waypoints pasados
		for (int i = 0; i < 4; i++) {
			GUI.DrawTexture (new Rect (Screen.width * 8.7f / 10, Screen.height * 2 / 10 + positionY * i, iconSize / 2, iconSize / 3), progress [0], ScaleMode.ScaleToFit);
		}
		//Waypoints pass
		//Cambiar 4 por número de waypoints
		//Cambiar 3 por número de waypoints pasados - numero total
		for (int i = 3; i < 4; i++) {
			GUI.DrawTexture (new Rect (Screen.width * 8.7f / 10, Screen.height * 2 / 10 + positionY * i, iconSize / 2, iconSize / 3), progress [1], ScaleMode.ScaleToFit);
		}
		//Spaceship
		GUI.DrawTexture (new Rect (Screen.width * 8.8f / 10, Screen.height * 2 / 10 + iconSize * 3, iconSize / 2.8f, iconSize / 2.8f), progress [0], ScaleMode.ScaleToFit);
	}
}
