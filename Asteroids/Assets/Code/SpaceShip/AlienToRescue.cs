using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienToRescue : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "SpaceShip")
        {
            StoryLevelManager levelMgr = FindObjectOfType<StoryLevelManager>();
            levelMgr.GetAlien();
            gameObject.SetActive(false);
        }
    }

}
