using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	#region Public Attributes
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
        gameManagerSingleton = GameManagerSingleton.GetInstance();
		aS = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	protected virtual void Update () 
	{

	}
	#endregion

	#region User Methods

	#endregion
}
