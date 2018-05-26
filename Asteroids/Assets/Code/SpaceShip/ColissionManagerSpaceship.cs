using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColissionManagerSpaceship : MonoBehaviour {
    #region Public Attributes
    public GameObject explosion;
    #endregion

    #region Private Attributes
    private BaseLevelManager levelMgr;
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Start () {
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
        //explosion.SetActive(true);

        //
        /*if (particleSystem != null)
            particleSystem.Play();*/

    }
	#endregion

}
