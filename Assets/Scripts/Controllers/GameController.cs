using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private GameObject pausePanel;
	private static GameObject deathOverlay;
	private static GameObject hurtOverlay;
	private static GameObject winOverlay;
	private static bool paused = false;
	private static bool dead = false;

	// Use this for initialization
	void Start () {
		pausePanel = GameObject.Find ("PausePanel");
		pausePanel.SetActive (paused);
		deathOverlay = GameObject.Find ("DeathOverlay");
		HideDeathScreen ();
		hurtOverlay = GameObject.Find ("HurtOverlay");
		HideHurtScreen ();
		winOverlay = GameObject.Find ("WinOverlay");
		HideWinScreen ();
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

	public static bool isPaused() {
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

	public static void HideWinScreen() {
		winOverlay.SetActive (false);
	}
	
	public static void ShowWinScreen() {
		winOverlay.SetActive (true);
		dead = true;
	}

	public void ToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}

	public void ToGameScreen() {
		Application.LoadLevel ("GameScreen");
		dead = false;
	}
}
