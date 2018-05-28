using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeKit : MonoBehaviour {

    #region Public Methods
    //
    public int amountToHeal = 50;
    #endregion

    #region Monobehaviour Methods
    //
    private void OnTriggerEnter(Collider other)
    {
        // Detect if the collision happens with the spaceship,
        // heal it the specified amount
        // and progress in the level
        if(other.transform.name == "SpaceShip")
        {
            SurvivalLevelManager levelMgr = FindObjectOfType<SurvivalLevelManager>();
            levelMgr.SpaceCurrentLife += amountToHeal;
            gameObject.SetActive(false);
        }
    }
    #endregion
}
