using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroller : MonoBehaviour {

	public float ScrollSpeed = 2f;
	private SpriteRenderer sr;

	float offset = 0f;

	void Start ()
	{
		sr = GetComponent<SpriteRenderer> ();
	}

	void Update () 
	{
		offset = Mathf.Repeat (Time.time * ScrollSpeed, 1f);
		//逆向きなら//1f - offset;
		sr.material.SetTextureOffset("_MainTex",new Vector2(0f,offset));
	}
}
