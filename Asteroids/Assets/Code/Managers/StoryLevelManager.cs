using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLevelManager : BaseLevelManager {

    #region Private Attributes
    protected StoryLevelData levelData;
    protected List<GameObject> playerWayPoints;
    #endregion

    #region MonoBehaviour Methods
    // Use this for initialization
    protected override void Start () {
        base.Start();
        // Set the game mode
        gameManager.CurrentGameMode = GameMode.Story;
        //
        levelData = gameManager.CurrentStoryLevelData;
        // Set the little aliens
        for(int i = 0; i < levelData.numberOfAliens; i++)
        {
            Debug.Log("Spawning little alien");
        }
    }

    // Update is called once per frame
    protected override void Update () {
		
	}
    #endregion

    #region Methods



    #endregion
}
