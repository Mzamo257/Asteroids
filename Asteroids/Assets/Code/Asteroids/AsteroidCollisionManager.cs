using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollisionManager : MonoBehaviour {
	#region Public Attributes
	public float maxHealth = 100;
	public int type;
	#endregion

	#region Private Attributes
	private ParticleSystem asteroidParticleSystem;
    private AsteroidManager asteroidMgr;
    private Effects soundEffectsManager;
	private float collisionCounter = 0;
	private float health;
    #endregion

    #region Properties
    public AsteroidManager AsteroidMgr { set { asteroidMgr = value; } }
    #endregion

    #region MonoDevelop Methods
    void Start () 
	{
        asteroidParticleSystem = gameObject.GetComponent<ParticleSystem>();
        soundEffectsManager = FindObjectOfType<Effects>();
		health = maxHealth;
	}

	// Update is called once per frame
	void Update () 
	{
		collisionCounter += Time.deltaTime;
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	private void OnCollisionEnter(Collision collision)
	{
		//to avoid the collision and division of the asteroids in a small period of time, so it doesn't create amounts of asteoids. 
		if (collisionCounter < 1) 
		{
			return;
		}

        Rigidbody otherRigid = collision.rigidbody;
        if (otherRigid != null)
        {
            // We use the relative velocity of the collision to determine the damage
            health -= (collision.relativeVelocity.magnitude * otherRigid.mass);
			if (asteroidParticleSystem != null)
			{
				asteroidParticleSystem.Play ();
			}

            if (health <= 0)
            {
                soundEffectsManager.playEffect(0);
                gameObject.SetActive(false);
                asteroidMgr.CurrentActiveAsteroids--;
                DestroyAsteroid();
            }
            else
            {
                soundEffectsManager.playChoque();
            }
        }
	}
	#endregion

	#region User Methods
	/// <summary>
	/// Devide the asteroid into the next type.
	/// </summary>
	private void DestroyAsteroid()
	{
		int nextAsteroidType=type/2;
		asteroidMgr.ActivateAsteroidFromDivision (nextAsteroidType, transform.position);
		asteroidMgr.ActivateAsteroidFromDivision (nextAsteroidType, transform.position);
	}

	/// <summary>
	/// Resets the counter to control the number of collisions.
	/// </summary>
	public void ResetCounter()
	{
		collisionCounter = 0;
	}

	/// <summary>
	/// Restarts the health of the asteroid.
	/// </summary>
	public void RestartHealth()
	{
		health = maxHealth;
	}
	#endregion

}
