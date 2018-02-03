using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour {

	public void nextScene(){
		SceneManager.LoadScene("GGJ2018");
	}

	public void creditScene(){
		SceneManager.LoadScene ("Credits");
	}

	public void titleScene(){
		SceneManager.LoadScene ("Title");
	}

}
