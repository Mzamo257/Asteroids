using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollisionManager : MonoBehaviour {

    public float health = 100;

    private ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        float otherObjectMass = collision.rigidbody.mass;
        // Usamos la velocidad relativa de la colisión para determinar el daño
        health -= (collision.relativeVelocity.magnitude * otherObjectMass);
        Debug.Log(health);

        //
        /*if (particleSystem != null)
            particleSystem.Play();*/

        // 
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
            
    }
}
