using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuScript : MonoBehaviour {

    public Glass Glass;
    public MixologyVerb Verb;

	private bool isStirButton;
	private bool isShakeButton;
	private bool isIgniteButton;
	private bool isSquishButton;
	private bool isClose;

	private Transform parent;
	private Vector3 lastPos;

	// Use this for initialization
	void Start () {

		parent = this.transform.parent;
		lastPos = this.transform.parent.position;

		if (this.gameObject.name == "StirButton")
			isStirButton = true;
		if (this.gameObject.name == "ShakeButton")
			isShakeButton = true;
		if (this.gameObject.name == "IgniteButton")
			isIgniteButton = true;
		if (this.gameObject.name == "SquishButton")
			isSquishButton = true;
		if (this.gameObject.name == "ActionMenu")
			isClose = true;
			
	}

	void Update() {

		if ((parent.position != lastPos)&&isClose) {
			this.gameObject.SetActive (false);
		}

	}

	void OnMouseDown(){

		if (isStirButton) {
			Debug.Log ("Button is stirring it up.");
			parent.gameObject.SetActive (false);
		} else if (isShakeButton) {
			Debug.Log ("Button is Shaking it up!");
			parent.gameObject.SetActive (false);
		} else if (isIgniteButton) {
			Debug.Log ("Button is igniting it up!");
			parent.gameObject.SetActive (false);
		} else if (isSquishButton) {
			Debug.Log ("Button is squish it up!");
			parent.gameObject.SetActive (false);
		} 

		else if (isClose)
			this.gameObject.SetActive (false);
		else
			Debug.Log ("Somethings up with the action buttons! ACK!");

        Glass.Apply(Verb);
	} 

}
