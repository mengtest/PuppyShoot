using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SwitchGameScene : MonoBehaviour {

	public void EnterGameScene()
	{
		SceneManager.LoadScene("Level");
	}

	public void ExitGame()
	{

		Application.Quit ();
		Debug.Log ("quit");
	}

	public void ReplayGame()
	{
        SceneManager.LoadScene("Level");
	}

	public void BackToMainMenu()
	{
        SceneManager.LoadScene("Begin");
	}
}