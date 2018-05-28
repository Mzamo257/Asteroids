using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryLevelManager : BaseLevelManager {

    #region Public Attributes

    public GameObject alienPrefab;
	public GameObject trashPrefab;
	public Camera minimapCamera;

    #endregion

    #region Private Attributes
    protected StoryLevelData levelData;
    protected List<GameObject> playerWayPoints;
	protected int amountTrash = 10;
	protected int availableTrash = 10;
    protected int wayPointIndex;
    //
    protected List<GameObject> aliens;
    protected int caughtAliens = 0;
	private HUDstory hud;

	protected StoryIntro intro;

    #endregion

    #region Properties

    public int AliensToCatch { get { return levelData.numberOfAliens; } }

    public int CaughtAliens { get { return caughtAliens; } }

	public int AvailableTrash { get { return availableTrash; } }

	public int TotalTrash { get { return amountTrash; } }

	public GameObject CurrentWaypoint
    {
        get
        {
            if (playerWayPoints[wayPointIndex].activeInHierarchy)
                return playerWayPoints [wayPointIndex];
			else
                return null;
        }
    }

    #endregion

    #region MonoBehaviour Methods
    // Use this for initialization
    protected override void Start () {
        
        base.Start();
        //
        intro = GetComponent<StoryIntro>();
		intro.StartEvent ();
        // Set the game mode
        gameManagerSingleton.CurrentGameMode = GameMode.Story;
        // Get the level data
        levelData = gameManagerSingleton.CurrentStoryLevelData;
        // Set the little aliens
        aliens = new List<GameObject>();
        for(int i = 0; i < levelData.numberOfAliens; i++)
        {
            // Debug.Log("Spawning little alien");
            if (alienPrefab != null)
            {
                // Revisar como organizarse con los límites
                float x = Random.Range(-80.0f, 80.0f);
                float z = Random.Range(0.0f, 180.0f);
                Vector3 alienPosition = new Vector3(x, 0, z);
                GameObject newAlien = Instantiate(alienPrefab, alienPosition, Quaternion.identity);
                aliens.Add(newAlien);
            }
        }
        //
		playerWayPoints = new List<GameObject>(levelData.numberOfTrash);
        for (int i = 0; i < levelData.numberOfTrash; i++)
        {
            GameObject newWaypoint = Instantiate(trashPrefab, Vector3.zero, Quaternion.identity);
            newWaypoint.SetActive(false);
            playerWayPoints.Add(newWaypoint);
        }
        //
        hud = FindObjectOfType<HUDstory> ();

        // Assign the rest of the data
        // NOTE: This should be in BaseLevelManager, have to find the way
        asteroidManager.numAsteroids = levelData.numberOf_Asteroids;
        ship.force = levelData.force_Spaceship;
        ship.maxSpeed = levelData.max_Speed_Spaceship;

    }

    // Update is called once per frame
    protected override void Update ()
    {
		base.Update ();
        //check if the player has clicked in the minimap to place a hook
		if (hud.checkClick ().x != -1) 
		{
			Vector3 newWaypointPosition = DetermineZoneToAppear ();
			if(AvailableTrash > 0)
			{
                if (SetWaypoint(newWaypointPosition))
                {
                    availableTrash--;
                }
			}
		}
	}
    #endregion

    #region Methods

    /// <summary>
    /// Get the alien and advance the index
    /// And declare victory in case of getting all of them
    /// </summary>
    public void GetAlien()
    {
        caughtAliens++;
        if(caughtAliens == levelData.numberOfAliens)
        {
            GameManagerSingleton.instance.StoryProgress();
            gameState = GameState.Victory;

            // Make here the point calculation instead of GetScoreFromWaypoints
            float trashConservation = (float)availableTrash / (float)amountTrash;
            score = (int)(100 * trashConservation);
            gameManagerSingleton.CurrentScore += score;
        }
    }

    /// <summary>
    /// Set a waypoint inside the level boundaries
    /// And adavance the index of available ones
    /// </summary>
    /// <param name="waypointPosition"></param>
    public bool SetWaypoint(Vector3 waypointPosition)
    {
        // Check that the waypoint position is in the limits
        // The limits are:
        //     x = -100, 100
        //     z = 0, 200
        //  Yeah, it is hardcoded
        if (waypointPosition.x < -100 || waypointPosition.x > 100 || waypointPosition.z < 0 || waypointPosition.z > 200) return false;
        
        if (availableTrash <= 0) return false;

        playerWayPoints[levelData.numberOfTrash - availableTrash].SetActive(true);
        playerWayPoints[levelData.numberOfTrash - availableTrash].transform.position = waypointPosition;
        return true;
    }

    /// <summary>
    /// Manage the waypoint reaching
    /// In order to advance the index
    /// Or go to the defeat if not hooks availbale
    /// </summary>
    public void ReachWaypoint()
    {
       
        if(availableTrash == 0)
        {
            gameState = GameState.Defeat;
        }
		else if(playerWayPoints[wayPointIndex].activeInHierarchy)
		{
			playerWayPoints [wayPointIndex].SetActive (false);
            wayPointIndex++;
		}
        
    }

    /// <summary>
    /// Convert the click in the minimap into world coordinates
    /// </summary>
    /// <returns></returns>
	Vector3 DetermineZoneToAppear()
	{
        //Get position of the waypoint
		float zDistance = (ship.transform.position - minimapCamera.transform.position).magnitude;
		Vector2 cameraPoint = hud.checkClick ();
        //Convert into wolrd position
		Vector3 worldPoint = minimapCamera.ViewportToWorldPoint(new Vector3(cameraPoint.x, cameraPoint.y, zDistance));
        //Fix y
		worldPoint.y = ship.transform.position.y;

		return worldPoint;
	}

    #endregion
}
