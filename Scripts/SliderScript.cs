using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour {

	public GameObject slideTo;
	public float speed = .5f;
	public AudioClip bossGrumble;
	private Vector3 startingPos;
	private Vector3 destinationPos;
	private Vector3 awayPos;
	private Vector3 awayBossPos;
	private float fraction = 0; 
	private float timer = 0f;

	public bool slideIn;
	public bool slideOut;
	public bool isAlien;
	public bool isBoss;

	// Use this for initialization
	void Start () {
		if (isAlien)
			slideTo = GameObject.FindGameObjectWithTag ("SlideLoc");
		
		startingPos = gameObject.transform.position;
		destinationPos = slideTo.transform.position;
		awayPos = new Vector3 (destinationPos.x-20f, destinationPos.y, destinationPos.z);
		awayBossPos = new Vector3 (destinationPos.x + 20f, destinationPos.y, destinationPos.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (slideIn) {
			Slider (startingPos, destinationPos);
		}
			


		timer += Time.deltaTime;

		if (slideOut) {
			if (isBoss)
				Slider (destinationPos, awayBossPos);
			else
				Slider (destinationPos, awayPos);
		}
	}

	void Slider(Vector3 startPosition,Vector3 endPosition) 
    {
        if (fraction == 0 && slideIn && isBoss)
        {
            GetComponent<AudioSource>().PlayOneShot(bossGrumble);

        }

		if (fraction < 1) {
			fraction += Time.deltaTime * speed;
			transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0f,1.0f,fraction));
		}

		if (transform.position == endPosition) {
			if (slideOut) 
			{
				slideOut = false;
				if (isAlien)
					Destroy (this.gameObject);
                else if (isBoss)
                {
                    UIManager.Instance.ManagerSpeechBubble.gameObject.SetActive(false);
                }
			}

			if (slideIn)
            {
                slideIn = false;

				
                var draggables = GetComponentsInChildren<Draggable>();
                foreach (var draggable in draggables)
                {
                    draggable.ResetStartingPosition();
                }
                if (isBoss)
                {
                    slideOut = true;
                }
            }
			


			fraction = 0;
		}
	}
}
