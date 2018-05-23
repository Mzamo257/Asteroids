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
	private float collisionCounter = 0;
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
		collisionCounter += Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collisionCounter < 1)
			return;
        Rigidbody otherRigid = collision.rigidbody;
        if (otherRigid != null)
        {
            // Usamos la velocidad relativa de la colisión para determinar el daño
            health -= (collision.relativeVelocity.magnitude * otherRigid.mass);
            if (particleSystem != null)
                particleSystem.Play();
            //
            if (health <= 0)
            {
                // TODO: Restaurarles vida cuando salgan de la pool
                			soundEffectsManager.playEffect(2);
                gameObject.SetActive(false);
                asteroidMgr.CurrentActiveAsteroids--;
                Debug.Log("Asteroid destroyed");
                DestroyAsteroid();
            }
            else
            {
                int effectNumber = (int)(Random.value * 2);
                soundEffectsManager.playEffect(effectNumber);
            }
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

	public void ResetCounter(){
		collisionCounter = 0;
	}
	#endregion

}
