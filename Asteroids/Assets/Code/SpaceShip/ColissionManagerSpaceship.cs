using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColissionManagerSpaceship : MonoBehaviour {
    #region Public Attributes
    #endregion

    #region Private Attributes
    private BaseLevelManager levelMgr;
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Start () {
        levelMgr = FindObjectOfType<BaseLevelManager>();
	}
	
	#endregion

	#region User Methods
    private void OnCollisionEnter(Collision collision)
    {
        // If the collision has a rigibody
        // is an asteroid
        // so we damage the spaceship
        Rigidbody otherRigid = collision.rigidbody;
        if(otherRigid != null)
        // Usamos la velocidad relativa de la colisión para determinar el daño
        levelMgr.DamageSpaceShip(collision.relativeVelocity.magnitude * otherRigid.mass);


    }
	#endregion

}
