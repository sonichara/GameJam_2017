using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour {
	//FadeSceneの読み込み準備
	private FadeScene myFade;
	string currentScene;

	void Start ()
	{
		myFade = GetComponent<FadeScene> ();

	}


	void Update ()
	{
		
		if (Input.GetKeyDown (KeyCode.Tab)) 
		{
			StartFadeOut ();
		}


		if (myFade.alfa >= 1.0f) {
			switch (currentScene) {
				case "Title":
					SceneManager.LoadScene ("TestPlayer_0");
					break;
				case "Main":
					SceneManager.LoadScene ("Rezard");
					break;
				case "Rezard":
					SceneManager.LoadScene ("Title");
					break;
			}
		}


		
	}

	public void StartFadeOut() {
		myFade.enabled = true;
		currentScene = SceneManager.GetActiveScene ().name;
	}
}
