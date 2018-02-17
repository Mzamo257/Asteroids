using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject gameManager = GameObject.Find ("GameManager");
		if (!gameManager) {
			Application.LoadLevel ("Preload");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
