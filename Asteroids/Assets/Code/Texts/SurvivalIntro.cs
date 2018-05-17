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
	public Texture[] conversationIcons;
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
	private BaseLevelManager level;
	#endregion

	#region Properties
	public int Step
	{
		get { return step; }
	}
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Start () 
	{
		eventText = GameFunctions.GetTextXML ("EVENTS", "EVENT", "SurvivalIntro");
		//fadeInOut = GameObject.Find ("FadeInOut");
		//Conversation Box zone
		conversationBoxZone = new Rect(0, Screen.height * 4/5, Screen.width, Screen.height * 1/5);
		level = FindObjectOfType<BaseLevelManager>();
		style = level.textStyle;
		if (GameManagerSingleton.instance.CurrentSurvivalLevel != 0) 
		{
			level.InIntro = false;
			step++;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		//Controls
		bool lClickDown = Input.GetMouseButtonDown (0);
		//
		if (lClickDown && step == 0) {
			if (currentText < eventText.Length - 1)
				currentText++;
			else {
				step++;
				level.StartedAsteroids = 4;
				level.InIntro = false;
			}
		}
	}

	//For the text
	void OnGUI()
	{
		if (step == 0) {
			GUI.DrawTexture (conversationBoxZone, conversationBox, ScaleMode.StretchToFill);
			GUI.Label (conversationBoxZone, eventText [currentText], style);
		}
	}
	#endregion

	#region User Methods
	//
	public void StartEvent()
	{
		step = 0;
	}
	#endregion
}
