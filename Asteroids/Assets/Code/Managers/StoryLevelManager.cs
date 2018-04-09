using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLevelManager : BaseLevelManager {

    #region Private Attributes
    protected List<GameObject> playerWayPoints;
    #endregion

    #region MonoBehaviour Methods
    // Use this for initialization
    protected override void Start () {
        base.Start();
        // Set the game mode
        gameManager.CurrentGameMode = GameMode.Story;
        // Set the little aliens
        for(int i = 0; i < 5/*THis from level data*/; i++)
        {

        }
    }

    // Update is called once per frame
    protected override void Update () {
		
	}
    #endregion

    #region Methods



    #endregion
}
