using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

	#region Public Attributes
	public AudioClip [] effects;
	#endregion

	#region Private Attributes
	private AudioSource aS;
	protected GameObject gameManager;
	protected GameManager gmScript;
	#endregion


	#region MonoDevelop Methods
	// Use this for initialization
	protected virtual void Start () {
		//gameManager = GameObject.Find ("GameManager");
		//gmScript = gameManager.GetComponent<GameManager> ();
        gmScript = FindObjectOfType<GameManager>();
		aS = GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	protected virtual void Update () 
	{

	}
	#endregion

	#region User Methods
	public void playEffect(int effectNumber)
	{
        // Debug.Log("Trying to play");
		if (!gmScript.mute) 
		{
			aS.clip = effects [effectNumber];
			aS.Play ();
		}
	}
	#endregion
}
