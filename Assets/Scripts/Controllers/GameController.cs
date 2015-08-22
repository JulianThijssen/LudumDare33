using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private GameObject pausePanel;
	private bool paused = false;
	
	// Use this for initialization
	void Start () {
		pausePanel = GameObject.Find ("PausePanel");
		pausePanel.SetActive (paused);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = true;
			pausePanel.SetActive (paused);
		}
		if (Input.GetKeyUp (KeyCode.Escape)) {
			paused = false;
			pausePanel.SetActive (paused);
		}
	}

	public bool isPaused() {
		return paused;
	}
	
	public void ToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}
}
