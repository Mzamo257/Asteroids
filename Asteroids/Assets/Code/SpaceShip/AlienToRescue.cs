﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienToRescue : MonoBehaviour {

    private StoryMovementSpaceship playerShip;
    private StoryLevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        playerShip = FindObjectOfType<StoryMovementSpaceship>();
        levelManager = FindObjectOfType<StoryLevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToLook = playerShip.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionToLook);
        // Tener en cuenta que usamos sqrmagnitude
        // Para hacer menos pesado en el update
        // Distancia al cuadrado
        float distanceToPlayer = (transform.position - playerShip.transform.position).sqrMagnitude;
        if (distanceToPlayer < 25)
        {
            levelManager.GetAlien();
            gameObject.SetActive(false);
        }
    }
}
