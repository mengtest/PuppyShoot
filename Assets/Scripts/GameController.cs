using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	public GameObject enemy;
	public GameObject player;
	public PlayerController playerScript;
	public float spawnTime=0.5f;
	public float spawnDelay=0.0f;
	public Vector3 spawnValues;

	public Text scoreText;

	public int hitCount=0;

	private float disbetweensp;  //distance between spawn point and player


	private Canvas failedMenu;
	// Use this for initialization

	void Awake()
	{
		failedMenu = GameObject.FindGameObjectWithTag ("FailedMenu").GetComponent<Canvas> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerScript = player.GetComponent<PlayerController> ();
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text>();
	}

	void Start () {
		failedMenu.enabled = false;
		InvokeRepeating("SpawnEnemy", spawnDelay, spawnTime);
	}

	void Update()
	{
		UpdateScore (hitCount);
	}
	

	void UpdateScore(int hitcount)
	{
		scoreText.text = "Score: " + hitcount * 100;
	}

	// Update is called once per frame
	void SpawnEnemy () {
		if (playerScript.isLive) {
			spawnValues.z = 0;
			while (true) {
				spawnValues.x = Random.Range (-7, 7);
				spawnValues.y = Random.Range (-4, 4);
				disbetweensp = Vector3.Distance (player.transform.position, spawnValues);
				if (disbetweensp <= 3.0f) {
					continue;
				} else {
					break;
				}
			}
			Instantiate (enemy, spawnValues, this.transform.rotation);
		} else {
			failedMenu.enabled=true;
		}
	}
}
