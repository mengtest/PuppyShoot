using UnityEngine;
using System.Collections;

public class MoveToEnemy : MonoBehaviour {
	public float speed;
	public Transform targetTransform;
	public Vector2 movedir;				//move direction
	public GameObject player;
	public PlayerController playerScript;

	// Use this for initialization
	void Start () {


		player = GameObject.FindGameObjectWithTag ("Player");
		
		playerScript = player.GetComponent<PlayerController> ();
		
		targetTransform = playerScript.targetEnemy;
		
		this.transform.LookAt (targetTransform.position);
		movedir = (targetTransform.position - this.transform.position).normalized;
		GetComponent<Rigidbody2D> ().velocity = movedir * speed;
	}


	
}