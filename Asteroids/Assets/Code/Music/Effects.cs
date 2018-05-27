using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

	#region Public Attributes
	public AudioClip [] effects;
	public AudioClip [] choque;
	#endregion

	#region Private Attributes
	private AudioSource aS;
    protected GameManagerSingleton gameManagerSingleton;
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	protected virtual void Start () 
	{
        gameManagerSingleton = GameManagerSingleton.instance;
		aS = GetComponent<AudioSource> ();
        aS.volume = gameManagerSingleton.Volume;
    }
	#endregion

	#region User Methods
	private void playEffect(AudioClip clip)
	{
		if (gameManagerSingleton.Music) 
		{
			aS.clip = clip;
			aS.volume = gameManagerSingleton.Volume;
			aS.Play ();
		}
	}

	public void playEffect(int clip)
	{
		if (gameManagerSingleton.Music) 
		{
			aS.clip = effects[clip];
			aS.volume = gameManagerSingleton.Volume;
			aS.Play ();
		}
	}

	public void playChoque()
	{
        //Select a random effect
		int i = Random.Range(0, choque.Length);
		playEffect (choque [i]);
    }
    #endregion
}
