using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollisionManager : MonoBehaviour {
	#region Public Attributes
	public float health = 100;
	public int type;
	#endregion

	#region Private Attributes
	private ParticleSystem particleSystem;
    private AsteroidManager asteroidMgr;
    private Effects soundEffectsManager;
    #endregion

    #region Properties
    public AsteroidManager AsteroidMgr { set { asteroidMgr = value; } }
    #endregion

    #region MonoDevelop Methods
    // Use this for initialization
    void Start () {
		particleSystem = gameObject.GetComponent<ParticleSystem>();
        soundEffectsManager = FindObjectOfType<Effects>();
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log ("colisionado");
		float otherObjectMass = collision.rigidbody.mass;
		// Usamos la velocidad relativa de la colisión para determinar el daño
		health -= (collision.relativeVelocity.magnitude * otherObjectMass);
		// Debug.Log(health);
		/*if (particleSystem != null)
            particleSystem.Play();*/
		if(health <= 0)
		{
			// TODO: Restaurarles vida cuando salgan de la pool
//			soundEffectsManager.playEffect(2);
			gameObject.SetActive(false);
			DestroyAsteroid();
		}
		else
		{
			int effectNumber = (int)(Random.value * 2);
			soundEffectsManager.playEffect(effectNumber);
		}

	}
	#endregion

	#region User Methods
    

	private void DestroyAsteroid()
	{
		int nextAsteroidType=type/2;
		//Debug.Log (nextAsteroidType);
		asteroidMgr.ActivateAsteroidFromDivision (nextAsteroidType, transform.position);
		asteroidMgr.ActivateAsteroidFromDivision (nextAsteroidType, transform.position);
	}
	#endregion

}
