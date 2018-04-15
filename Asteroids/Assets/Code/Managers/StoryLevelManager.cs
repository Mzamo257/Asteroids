using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLevelManager : BaseLevelManager {

    #region Public Attributes

    public GameObject alienPrefab;

    #endregion

    #region Private Attributes
    protected StoryLevelData levelData;
    protected List<GameObject> playerWayPoints;
    //
    protected List<GameObject> aliens;
    protected int caughtAliens = 0;
    #endregion

    #region Properties

    public int AliensToCatch { get { return levelData.numberOfAliens; } }

    public int CaughtAliens { get { return caughtAliens; } }

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
        aliens = new List<GameObject>();
        for(int i = 0; i < levelData.numberOfAliens; i++)
        {
            // Debug.Log("Spawning little alien");
            if (alienPrefab != null)
            {
                Vector3 alienPosition = new Vector3(0, 0, 100 * i);
                GameObject newAlien = Instantiate(alienPrefab, alienPosition, Quaternion.identity);
                aliens.Add(newAlien);
            }
        }
		playerWayPoints = new List<GameObject>();
    }

    // Update is called once per frame
    protected override void Update () {
		
	}
    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    public void GetAlien()
    {
        caughtAliens++;
        if(caughtAliens == levelData.numberOfAliens)
        {
            Debug.Log("Victory");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="waypointPosition"></param>
    public void SetWaypoint(Vector3 waypointPosition)
    {
        // TODO: Manejar con pool
        GameObject newWaypoint = Instantiate(wayPoint_prefab, waypointPosition, Quaternion.identity);
        playerWayPoints.Add(newWaypoint);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ReachWaypoint()
    {
        playerWayPoints.RemoveAt(0);
    }

    #endregion
}
