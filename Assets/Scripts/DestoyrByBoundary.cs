using UnityEngine;
using System.Collections;

public class DestoyrByBoundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {

		if(other.gameObject.CompareTag("EnemyBullet")||other.gameObject.CompareTag("Bullet"))
		{
			Destroy(other.gameObject);
		}
	
	}
}
