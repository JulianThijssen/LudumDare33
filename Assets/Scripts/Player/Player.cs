using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public GameObject explosion;
	public int maxTime;

	public AudioClip music;
	public AudioClip hurt_audio;
	public AudioClip death_audio;

	private float timer;
	private Text timerText;

	private bool exploded = false;
	private bool hurt = false;
	private bool dead = false;
	private bool atTarget = false;

	private float hurtCooldown = 1;
	private float hurtTime = 0;

	private int lives = 2;

	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint(music, transform.position);
		//transform.position = new Vector3 (300.5f, 49.19f, 233.03f);

		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		timer = maxTime;
	}
	
	// Update is called once per frame
	void Update () {
		// Respawn cheat key
		if (Input.GetKey (KeyCode.R)) {
			Respawn ();
		}

		// Explosion
		if (!exploded) {
			if (Input.GetKey (KeyCode.F)) {
				Explode ();
			}
		}

		// Hurt cooldown
		if (hurt) {
			hurtTime += Time.deltaTime;

			if (hurtTime > hurtCooldown) {
				hurt = false;
				hurtTime = 0;
				GameController.HideHurtScreen();
			}
		}

		// Timer
		if (timer > 0) {
			timer -= Time.deltaTime;

			timerText.text = timer.ToString ("F0");
		} else {
			Explode ();
		}

		if (lives <= 0) {
			Die ();
		}
	}

	void Respawn () {
		GameObject.Find ("GameController").GetComponent<GameController>().ToGameScreen ();
	}

	void Explode() {
		if (exploded) {
			return;
		}

		exploded = true;
		Instantiate (explosion, transform.position, Quaternion.identity);
		Instantiate (explosion, transform.position, Quaternion.identity);
		Instantiate (explosion, transform.position, Quaternion.identity);

		if (atTarget) {
			Win ();
		} else {
			Die ();
		}
	}

	void Win() {
		GameController.ShowWinScreen ();
	}

	void Die() {
		if (!dead) {
			GameController.ShowDeathScreen ();
			dead = true;
			AudioSource.PlayClipAtPoint(death_audio, transform.position);
		}
	}

	void Hurt() {
		if (!hurt) {
			GameController.ShowHurtScreen();
			hurt = true;
			lives--;

			if (lives > 0) {
				AudioSource.PlayClipAtPoint(hurt_audio, transform.position);
			}
		}
	}
	
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Target") {
			atTarget = true;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.tag == "Target") {
			atTarget = false;
		}
	}
	
	void OnTriggerStay(Collider collider) {
		if (collider.gameObject.tag == "Hurt") {
			Hurt ();
		}
		if (collider.gameObject.tag == "Death") {
			Die ();
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Prop") {
			Hurt ();
		}
	}
}
