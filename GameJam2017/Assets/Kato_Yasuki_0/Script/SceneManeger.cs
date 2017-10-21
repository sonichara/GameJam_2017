using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour {
	//FadeSceneの読み込み準備
	private FadeScene myFade;
	void Start ()
	{
		myFade = GetComponent<FadeScene> ();

	}


	void Update ()
	{
		/*
		if (Input.GetKeyDown (KeyCode.Tab)) 
		{
			StartFadeOut ();
		}
		*/

		if (myFade.alfa >= 1.0f) {
			SceneManager.LoadScene ("Main");
		}
	}

	public void StartFadeOut() {
		myFade.enabled = true;
	}
}
