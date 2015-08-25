using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			ToGameScreen();
		}
		if (Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.Q)) {
			Application.Quit();
		}
	}

	public void ToGameScreen() {
		Application.LoadLevel ("GameScreen");
	}
}
