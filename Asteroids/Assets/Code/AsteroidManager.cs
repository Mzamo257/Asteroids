﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    private GameObject[] asteroids;
    // private List<GameObject> asteroids;

	// Use this for initialization
	void Start () {
        // Method 1: 
        // Asteroids in the scene by hand and got by FindGameObjectsWithTag
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        Debug.Log(asteroids.Length);
	}
	
	// Update is called once per frame
	void Update () {
        // TODO: Mover este chequeo a una corrutina
        CheckAsteroids();
	}

    #region User Methods

    /// <summary>
    /// Haz una pasada comprobando los asteriodes
    /// De momento que estén en pantalla
    /// </summary>
    void CheckAsteroids()
    {
        for(int i = 0; i < asteroids.Length; i++)
        {
            // Check if any of them is out of screen
            if (CheckOutOfCamera(asteroids[i])) {
                asteroids[i].transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                asteroids[i].GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
    }

    /// <summary>
    /// Check if the asteroid is out of the camera
    /// </summary>
    /// <param name="asteroid"></param>
    /// <returns></returns>
    bool CheckOutOfCamera(GameObject asteroid)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(asteroid.transform.position);
        return screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;
    }

    #endregion
}