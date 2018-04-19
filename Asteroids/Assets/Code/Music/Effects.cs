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
	protected GameObject gameManager;
	protected GameManager gmScript;
	#endregion


	#region MonoDevelop Methods
	// Use this for initialization
	protected virtual void Start () 
	{
        gmScript = FindObjectOfType<GameManager>();
		aS = GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	protected virtual void Update () 
	{

	}
	#endregion

	#region User Methods
	public void playEffect(int clip)
	{
		if (!gmScript.mute) 
		{
			//aS.clip = clip;
			aS.volume = gmScript.Volume;
			aS.Play ();
		}
	}

	public void playChoque()
	{
		int i = /*random*/0;
		//playEffect (choque [i]);
	}
	#endregion
}
