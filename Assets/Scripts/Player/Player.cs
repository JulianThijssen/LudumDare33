using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Vector3 spawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R)) {
			Respawn ();
		}
	}

	void Respawn () {
		transform.position = spawn;
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Hurt") {
			Debug.Log ("Ouch!");
		}
	}
}
