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
    protected GameManagerSingleton gameManagerSingleton;
	#endregion


	#region MonoDevelop Methods
	// Use this for initialization
	protected virtual void Start () {
        gameManagerSingleton = GameManagerSingleton.instance;
		aS = GetComponent<AudioSource> ();
        aS.volume = gameManagerSingleton.Volume;
        PlayMusic();
	}
    
	#endregion

	#region User Methods

    /// <summary>
    /// Play the music clip assigned to the scene
    /// </summary>
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
            case "Victory":
                aS.clip = musicClips[0];
                break;
        }
        aS.Play();
    }

	#endregion
}
