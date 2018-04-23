using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class SurvivalIntro : MonoBehaviour {

	#region Public Attributes
	//Common ones
	public string name;
	public Texture[] conversationIcons;
	public Texture conversationFrame;
	public Texture conversationBox;
	#endregion

	#region Private Attributes
	//Common ones
	private GameObject fadeInOut;
	private Rect conversationZone;
	private Rect conversationBoxZone;
	//Specific ones
	private int step = -1;				//For controlling the progress trough the scene
	private string[] eventText;
	private int currentText = 0;
	private GUIStyle style;
	private GameManagerSingleton gameManagerSingleton;
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Start () 
	{
		gameManagerSingleton = GameManagerSingleton.GetInstance ();
		eventText = GameFunctions.GetTextXML ("EVENTS", "EVENT", "SurvivalIntro");
		//fadeInOut = GameObject.Find ("FadeInOut");
		//Conversation Box zone
		conversationBoxZone = new Rect(0, Screen.height * 4/5, Screen.width, Screen.height * 1/5);
		//style = gmControl.GetStyle ();
	}

	// Update is called once per frame
	void Update () 
	{
		float dt = Time.deltaTime;
		//Controls
		bool lClickDown = Input.GetMouseButtonDown (0);
		//
		switch (step) 
		{
		case 0:	
			if (lClickDown) {
				currentText++;
				if (currentText == 1)
					step++;
			}
			break;
		case 1:					//Little conversation
			if (lClickDown) 
			{
				currentText++;
				if(currentText == 2)
					step++;
			}
			break;
		case 2:	
			//
			step++;
			break;
		case 3:
			break;
		case 4:					//It speaks
			if (lClickDown) 
			{
				step++;
			}
			break;
		case 5:	
			step++;
			break;
		case 6:					//Start the fade
			fadeInOut.SendMessage ("Switch");
			step++;

			break;
		case 7:					//Progress the game and reset scene
			FadeInOut fadeInOutScript = fadeInOut.GetComponent<FadeInOut> ();
			if (fadeInOutScript.GetAlpha () >= 1.0f) 
			{
				step++;
			}
			break;
		}
	}

	//For the text
	void OnGUI()
	{
			GUI.DrawTexture (conversationBoxZone, conversationBox, ScaleMode.StretchToFill);
			GUI.Label (conversationBoxZone, eventText [currentText]);
	}
	#endregion

	#region User Methods
	//
	public void StartEvent()
	{
		step = 0;
	}

	/*//To put it at the side of speaker's head
	public void EstablishConversationIconZone(Vector3 speakerPosition)
	{
		//
		GameObject hud = GameObject.Find("HUD");
		HUD hControl = hud.GetComponent<HUD> ();
		//float iconSize = hControl.GetIconSize ();
		//
		Vector3 headPoint = new Vector3 (0.0f, -2.0f, 0.0f);
		Vector2 position = Camera.main.WorldToScreenPoint (speakerPosition + headPoint);
		//conversationZone = new Rect (position.x, position.y, iconSize, iconSize);
	}*/
	#endregion
}
