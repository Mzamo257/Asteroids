using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseIntro : MonoBehaviour {

    #region Public Attributes
    //Common ones
    public Texture[] conversationIcons;
    public Texture conversationBox;
    #endregion

    #region Private Attributes
    //Common ones
    protected Rect iconZone;
    protected Rect conversationBoxZone;
    //Specific ones
    protected int step = -1;              //For controlling the progress trough the scene
    protected string[] eventText;
    protected int currentText = 0;
    protected GUIStyle style;
    protected BaseLevelManager level;
    protected string file;
    #endregion

    #region MonoDevelop Methods
    // Use this for initialization
    protected virtual void  Start()
    {
        eventText = GameFunctions.GetTextXML("EVENTS", "EVENT", file);
        //Conversation Box zone
        conversationBoxZone = new Rect(0, Screen.height * 4 / 5, Screen.width, Screen.height * 1 / 5);
        iconZone = new Rect(Screen.width * 7 / 10, Screen.height * 4.5f / 10, Screen.width * 2.5f/10, Screen.height * 4/10);
        level = FindObjectOfType<BaseLevelManager>();
        style = level.textStyle;
    }

    // Update is called once per frame
    protected void Update()
    {
        //Controls
        bool lClickDown = Input.GetMouseButtonDown(0);
        //
        if (lClickDown && step == 0)
        {
            if (currentText < eventText.Length - 1)
                currentText++;
            else
            {
                FinishEvent();
            }
        }
    }

    //For the text
    protected void OnGUI()
    {
        if (step == 0)
        {
            GUI.DrawTexture(conversationBoxZone, conversationBox, ScaleMode.StretchToFill);
            GUI.DrawTexture(iconZone, conversationIcons[currentText], ScaleMode.ScaleToFit);
            GUI.Label(conversationBoxZone, eventText[currentText], style);
        }
    }
    #endregion

    #region User Methods
    //
    public void StartEvent()
    {
        step = 0;
    }

    protected void FinishEvent()
    {
        step++;
        level.StartedAsteroids = 4;
        level.InIntro = false;
    }
    #endregion
}
