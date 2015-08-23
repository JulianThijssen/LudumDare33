using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Vector3 spawn;
	public GameObject explosion;

	private bool exploded = false;
	private bool hurt = false;
	private bool dead = false;
	private bool atTarget = false;

	private float hurtCooldown = 1;
	private float hurtTime = 0;

	private int lives = 2;

	// Use this for initialization
	void Start () {

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

		if (lives <= 0) {
			Die ();
		}
	}

	void Respawn () {
		GameObject.Find ("GameController").GetComponent<GameController>().ToGameScreen ();
	}

	void Explode() {
		exploded = true;
		GameObject exp = (GameObject) Instantiate (explosion, transform.position, Quaternion.identity);

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
		}
	}

	void Hurt() {
		if (!hurt) {
			GameController.ShowHurtScreen();
			hurt = true;
			lives--;
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
}
