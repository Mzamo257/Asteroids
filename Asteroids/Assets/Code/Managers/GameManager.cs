using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	#region Public Attributes
	#endregion

	#region Private Attributes
	private string language;
	private bool music;
	private float spaceshipLife = 100;
	private List<data_Level> levels_List;
    private float spaceshipCurrentLife;
	private int current_Level;
    //private float damage = 0;
	#endregion

	#region Properties
	public float SpaceCurrentLife {
		get { return spaceshipCurrentLife; }
	}

	public int Current_Level {
		get { return current_Level; }
	}

	public data_Level current_Level_data
	{
		get{ return levels_List[current_Level]; }
	}
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start () {
		language = "English";
		SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
        spaceshipCurrentLife = spaceshipLife;

		//Create the levels
		levels_List = new List<data_Level>();
		for(int i = 0; i< 2; i++)
		{
			levels_List.Add(Reader.getDataFromXML ("LEVEL1"));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#endregion

	#region User Methods

	public string GetLanguage()
	{
		return language;
	}

	public bool GetSound()
	{
		return music;
	}

	public float GetSpaceshipLife()
	{
		return spaceshipLife;
	}

    public void DamageSpaceShip( float damage)
    {
        spaceshipLife -= damage;
        spaceshipLife = Mathf.Max(0, spaceshipLife);
        if(spaceshipLife == 0)
        {
            // Aquí el gameover
        }
    }

	public void ChangeLanguage(int value)
	{
		switch (value) {
		case 0:
			language = "English";
			break;
		case 1:
			language = "Español";
			break;
		case 2:
			language = "Français";
			break;
		}
	}

	public void SetSound(bool change)
	{
		music = change;
	}

	/*public float GetDamage()
	{
		return damage;
	}*/
	#endregion
}
