using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDstory : HUD {

	#region Private Attributes

	#endregion

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
		GUI.Label (new Rect (Screen.width * 9 / 10, Screen.height / 20, Screen.width/10, Screen.height/10), "2" + " / " + "10");
	}
}
