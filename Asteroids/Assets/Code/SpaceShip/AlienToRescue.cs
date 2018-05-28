using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienToRescue : MonoBehaviour {

    #region Public Attributes
    public float catchDistance = 10;
    #endregion

    #region Private Attributes
    private StoryMovementSpaceship playerShip;
    private StoryLevelManager levelManager;
    #endregion

    #region Monobehaviour Methods

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
        // Have in mind that we use sqrmagnitude
        // To lighten the update
        // Square distance
        float distanceToPlayer = (transform.position - playerShip.transform.position).sqrMagnitude;
        if (distanceToPlayer < Mathf.Pow(catchDistance, 2))
        {
            levelManager.GetAlien();
            gameObject.SetActive(false);
        }
    }

    #endregion
}
