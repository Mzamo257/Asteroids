using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipColissionManager : MonoBehaviour {
	#region Public Attributes
	#endregion

	#region Private Attributes
	private GameManager gameMgr;
	#endregion

	#region MonoDevelop Methods
	// Use this for initialization
	void Start () {
        gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	#endregion

	#region User Methods
    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Spaceship collision");
        float otherObjectMass = collision.rigidbody.mass;
        // Usamos la velocidad relativa de la colisión para determinar el daño
        gameMgr.DamageSpaceShip(collision.relativeVelocity.magnitude * otherObjectMass);
        // Debug.Log(health);

        //
        /*if (particleSystem != null)
            particleSystem.Play();*/

    }
	#endregion

}
