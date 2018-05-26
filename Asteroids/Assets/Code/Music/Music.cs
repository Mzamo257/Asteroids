using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    #region Public Attributes

    public AudioClip[] musicClips;

	#endregion

	#region Private Attributes
	private AudioSource aS;
	//protected GameObject gameManager;
	//protected GameManager gmScript;
    protected GameManagerSingleton gameManagerSingleton;
	#endregion


	#region MonoDevelop Methods
	// Use this for initialization
	protected virtual void Start () {
        //gameManager = GameObject.Find ("GameManager");
        //gmScript = gameManager.GetComponent<GameManager> ();
        gameManagerSingleton = GameManagerSingleton.instance;
		aS = GetComponent<AudioSource> ();
        aS.volume = gameManagerSingleton.Volume;
        PlayMusic();
	}

	// Update is called once per frame
	protected virtual void Update () 
	{

	}
	#endregion

	#region User Methods

    void PlayMusic()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                aS.clip = musicClips[0];
                break;
            case "Survival":
                aS.clip = musicClips[1];
                break;
            case "Story":
                aS.clip = musicClips[2];
                break;
        }
        aS.Play();
    }

	#endregion
}
