using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private GameObject pausePanel;
	private static GameObject deathOverlay;
	private bool paused = false;
	
	// Use this for initialization
	void Start () {
		pausePanel = GameObject.Find ("PausePanel");
		pausePanel.SetActive (paused);
		deathOverlay = GameObject.Find ("DeathOverlay");
		deathOverlay.SetActive (false);
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

	public static void HideDeathScreen() {
		deathOverlay.SetActive (false);
	}

	public static void ShowDeathScreen() {
		deathOverlay.SetActive (true);
	}

	public void ToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}
}
