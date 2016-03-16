using UnityEngine;
using System.Collections;

public class MoveToPlayer : MonoBehaviour {

	public GameObject explosion;
	public float speed;
	public GameObject player;
	public Transform targetTransform;
	public Vector2 movedir;


	// Use this for initialization
	void Start () {
			player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			targetTransform = player.transform;
			this.transform.LookAt (targetTransform.position);
			movedir = (targetTransform.position - this.transform.position).normalized;
			GetComponent<Rigidbody2D> ().velocity = movedir * speed;
		} else {
			Destroy(this.gameObject);
		}

	}
	void OnExplosion()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, this.transform.position, randomRotation);
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			OnExplosion();
			Destroy(other.gameObject);
			Destroy(this.gameObject);

		}
	}


}
