using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private GameObject pausePanel;
	private static GameObject deathOverlay;
	private static GameObject hurtOverlay;
	private bool paused = false;
	private static bool dead = false;

	// Use this for initialization
	void Start () {
		pausePanel = GameObject.Find ("PausePanel");
		pausePanel.SetActive (paused);
		deathOverlay = GameObject.Find ("DeathOverlay");
		HideDeathScreen ();
		hurtOverlay = GameObject.Find ("HurtOverlay");
		HideHurtScreen ();
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

	public static bool isDead() {
		return dead;
	}

	public static void HideHurtScreen() {
		hurtOverlay.SetActive (false);
	}
	
	public static void ShowHurtScreen() {
		hurtOverlay.SetActive (true);
	}

	public static void HideDeathScreen() {
		deathOverlay.SetActive (false);
	}

	public static void ShowDeathScreen() {
		deathOverlay.SetActive (true);
		dead = true;
	}

	public void ToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}

	public void ToGameScreen() {
		Application.LoadLevel ("GameScreen");
		Time.timeScale = 1;
		dead = false;
	}
}
