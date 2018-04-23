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

	public GameObject CurrentWaypoint { get { if (playerWayPoints.Count > 0)return playerWayPoints [0];
												else return null;} }

    #endregion

    #region MonoBehaviour Methods
    // Use this for initialization
    protected override void Start () {
        base.Start();
		intro = GetComponent<StoryIntro>();
		intro.StartEvent ();
        // Set the game mode
        gameManagerSingleton.CurrentGameMode = GameMode.Story;
        //
        levelData = gameManagerSingleton.CurrentStoryLevelData;
        // Set the little aliens
        aliens = new List<GameObject>();
        for(int i = 0; i < levelData.numberOfAliens; i++)
        {
            // Debug.Log("Spawning little alien");
            if (alienPrefab != null)
            {
                // Revisar como organizarse con los límites
                float x = Random.RandomRange(-80.0f, 80.0f);
                float z = Random.RandomRange(0.0f, 180.0f);
                Vector3 alienPosition = new Vector3(x, 0, z);
                GameObject newAlien = Instantiate(alienPrefab, alienPosition, Quaternion.identity);
                aliens.Add(newAlien);
            }
        }
		playerWayPoints = new List<GameObject>();

		hud = FindObjectOfType<HUDstory> ();
    }

    // Update is called once per frame
    protected override void Update () {
		base.Update ();
		if (hud.checkClick ().x != -1) 
		{
			Vector3 newWaypointPosition = DetermineZoneToAppear ();
			if(AvailableTrash > 0)
			{
				availableTrash--;
				GameObject newTrash = Instantiate (trashPrefab, newWaypointPosition, Quaternion.identity);
				playerWayPoints.Add (newTrash);
			}
		}
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
            GameManagerSingleton.GetInstance().StoryProgress();
			win = true;
            gameState = GameState.Victory;
            Time.timeScale = 0.0f;
			//PauseGame ();
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
		if (playerWayPoints.Count > 0) 
		{
			playerWayPoints [0].SetActive (false);
			playerWayPoints.RemoveAt (0);
		}
    }

	Vector3 DetermineZoneToAppear()
	{
		float zDistance = (ship.transform.position - minimapCamera.transform.position).magnitude;
		Vector2 cameraPoint = hud.checkClick ();
		Vector3 worldPoint = minimapCamera.ViewportToWorldPoint(new Vector3(cameraPoint.x, cameraPoint.y, zDistance));
		worldPoint.y = ship.transform.position.y;
		return worldPoint;
	}
		
	public override bool introEnd()
	{
		if (intro.Step == 1)
			return true;
		return false;
	}

    #endregion
}
