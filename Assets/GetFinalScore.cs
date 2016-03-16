using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetFinalScore : MonoBehaviour {

	public GameController gameController;
	private Text finalScore;
	public 
	// Use this for initialization
	void Awake()
	{
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		finalScore = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		finalScore.text = "Final "+gameController.scoreText.text;
	}
}
