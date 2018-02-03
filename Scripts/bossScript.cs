using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour {

	public float bossTimer;
	SliderScript sliderScript;

	private float timer = 0f;
	private bool slidingOut;

	// Use this for initialization
	void Start () {
		sliderScript = GetComponent<SliderScript> ();
		slidingOut = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if ((timer >= bossTimer)&&(!slidingOut)) {
			sliderScript.slideOut = true;
			slidingOut = true;
		}
			
			
	}
}
