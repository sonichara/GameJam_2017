using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour {

	[SerializeField]
	Sprite ON = null;
	[SerializeField]
	Sprite OFF = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void On(bool isOn) {
		GetComponent<Image> ().sprite = isOn ? ON : OFF;
	}
}
