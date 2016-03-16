using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private ParticleSystem explosionFX;
	public GameObject explosion;
	public GameObject rocketEnemy;
	public float spawnTime=3.0f;
	public float spawnDelay=0.0f;

	private  Animator anim;

	private GameController gameController;

	void Awake()
	{
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
		anim = GetComponent<Animator> ();
	}

	void Start()
	{
		anim.SetTrigger ("Borned");
		InvokeRepeating("Shoot", spawnDelay, spawnTime);
	}

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log (other.gameObject.name);
		if (other.gameObject.CompareTag ("Bullet")) 
		{

			DestroyObject(other.gameObject);
			DestroyObject(this.gameObject);
			explosionFX.transform.position = transform.position;
			OnExplosion();
			explosionFX.Play();
			gameController.hitCount++;
		}
		
	}

	void OnExplosion()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}

	void Shoot()
	{
		Instantiate (rocketEnemy, this.transform.position, this.transform.rotation);
	}
	

}
