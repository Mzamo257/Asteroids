using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColissionManagerSpaceship : MonoBehaviour {
	#region Public Attributes
	#endregion

	#region Private Attributes
	// private GameManager gameMgr;
    private GameManagerSingleton gameManagerSingleton;
    private BaseLevelManager levelMgr;
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Start () {
        // gameMgr = FindObjectOfType<GameManager>();
        gameManagerSingleton = GameManagerSingleton.instance;
        levelMgr = FindObjectOfType<BaseLevelManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	#endregion

	#region User Methods
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigid = collision.rigidbody;
        if(otherRigid != null)
        // Usamos la velocidad relativa de la colisión para determinar el daño
        levelMgr.DamageSpaceShip(collision.relativeVelocity.magnitude * otherRigid.mass);

        //
        /*if (particleSystem != null)
            particleSystem.Play();*/

    }
	#endregion

}
