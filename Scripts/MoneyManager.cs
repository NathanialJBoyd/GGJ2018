using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

	public Text moneyText;
	public int money;
	private string moneyFormatted;

	// Use this for initialization
	void Start () {
		moneyText = gameObject.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		moneyFormatted = string.Format("{0:#,###0}", money);
		moneyText.text = moneyFormatted;
	}
}

