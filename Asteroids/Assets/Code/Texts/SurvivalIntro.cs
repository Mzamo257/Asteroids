using UnityEngine;

public class SurvivalIntro : BaseIntro {

	#region MonoDevelop Methods
	// Use this for initialization
	protected override void Start () 
	{
        file = "SurvivalIntro";
        base.Start();
		if (GameManagerSingleton.instance.CurrentSurvivalLevel != 0) 
		{
            FinishEvent();
		}
	}
	#endregion
}
