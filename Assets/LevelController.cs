using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGameSence()
    {
        
        Application.LoadLevel("Battle");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
