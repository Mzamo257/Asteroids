using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeKit : MonoBehaviour {

    //
    public int amountToHeal = 50;

    //
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "SpaceShip")
        {
            SurvivalLevelManager levelMgr = FindObjectOfType<SurvivalLevelManager>();
            levelMgr.SpaceCurrentLife += amountToHeal;
            gameObject.SetActive(false);
        }
    }
}
