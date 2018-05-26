using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIntro : BaseIntro {
    #region MonoDevelop Methods
    // Use this for initialization
    protected override void Start () 
	{
        file = "StoryIntro";
        base.Start();
		if (GameManagerSingleton.instance.CurrentStoryLevel != 0)
		{
            FinishEvent();
		}
	}
    #endregion
}
