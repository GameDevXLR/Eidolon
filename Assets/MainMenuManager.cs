using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void QuitTheGame()
	{
		Application.Quit ();
	}

	public void LoadFirstScene()
	{
		SceneManager.LoadScene (1);
	}
}
