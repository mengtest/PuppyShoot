using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Vector2 destinationPosition;	//the destination point
	private float destinationDistance;		//the destance between this.position and destinationPosition
	private Vector2 movedir;				//move direction
	public float moveSpeed;				//the speed the character will move

	public GameObject rocket;
	public Transform targetEnemy;

	public AudioClip death;

	private  Vector2 recoilForce;

	public bool isLive;

	// Use this for initialization
	void Start () {
		isLive = true;
		destinationPosition = this.transform.position;
		destinationDistance = 0;
		targetEnemy = null;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 
			if(hit.collider.CompareTag("Background"))
			{
				destinationPosition=hit.point;
			}else if(hit.collider.CompareTag("Enemy"))
			{
				targetEnemy=hit.transform;
				Instantiate(rocket,this.transform.position,this.transform.rotation);
				recoilForce=(targetEnemy.position-this.transform.position).normalized;
				GetComponent<Rigidbody2D>().AddForceAtPosition(-recoilForce*500,this.transform.position);
			}
		}
	}

	void FixedUpdate()
	{
		destinationDistance=Vector2.Distance(this.transform.position,destinationPosition);
		if (destinationDistance!= 0) {
			ClickToMove (moveSpeed);
		}
	}
    
	void ClickToMove(float speed)
	{	
		this.transform.position = Vector2.MoveTowards (this.transform.position, destinationPosition, Time.deltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("EnemyBullet")) 
		{
			isLive=false;
			AudioSource.PlayClipAtPoint(death,this.transform.position);
			GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
		}
	}


}