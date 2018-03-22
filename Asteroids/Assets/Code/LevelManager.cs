using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour {

	private List<Level> levels_List;

	// Use this for initialization
	void Start () {
		Reader.getDataFromXML ("LEVEL1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
