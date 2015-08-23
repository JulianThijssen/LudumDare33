using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Vector3 spawn;
	public GameObject explosion;

	private bool exploded = false;
	private bool hurt = false;
	private bool dead = false;

	private float hurtCooldown = 1;
	private float hurtTime = 0;

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
			}
		}
	}

	void Respawn () {
		Application.LoadLevel ("GameScreen");
	}

	void Explode() {
		exploded = true;
		GameObject exp = (GameObject) Instantiate (explosion, transform.position, Quaternion.identity);
		Die ();
	}

	void Die() {
		if (!dead) {
			GameController.ShowDeathScreen ();
			dead = true;
		}
	}

	void Hurt() {
		if (!hurt) {
			Debug.Log ("Ouch!");
			hurt = true;
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
