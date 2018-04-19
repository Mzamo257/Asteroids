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
	#endregion

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		minimapDimension = minimap.rectTransform;
		Debug.Log (minimapDimension.position + " " + minimapDimension.rect);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		checkClick ();
	}

	protected override void OnGUI()
	{
		base.OnGUI ();
		GUI.Label (new Rect (Screen.width * 9 / 10, Screen.height / 20, Screen.width/10, Screen.height/10), "num marcianos" + " / " + "num total marcianos");
		GUI.Label (new Rect (Screen.width * 7 / 10, Screen.height / 20, Screen.width/10, Screen.height/10), "numero basura" + " / " + "numero total basura");
	}

	public void checkClick()
	{
		if (Input.GetMouseButtonUp(0)) {
			Vector3 mPosition = Input.mousePosition;
			//Debug.Log ("Minimapa: " + mPosition + " Dimension: " + minimapDimension.position);
			if (mPosition.x >= minimapDimension.position.x && mPosition.y <= minimapDimension.position.y)
				//Debug.Log ("He clicado en " + mPosition);
		}
	}
}
