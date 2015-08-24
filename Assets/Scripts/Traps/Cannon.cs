using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	public GameObject cannonBall;
	public float period = 3f;
	public float force = 200000f;

	private float time = 0;

	public bool firing = false;

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!firing) {
			return;
		}

		transform.LookAt(player.transform.position);
		transform.Rotate (new Vector3 (4, 180, 0));

		time += Time.deltaTime;

		if (time > period) {
			GameObject ball = (GameObject) Instantiate (cannonBall, transform.Find ("SpawnCannonball").position, Quaternion.identity);
			ball.GetComponent<Rigidbody>().AddForce (-transform.forward * force);
			time = 0;
		}
	}

	public void OnAction() {
		firing = true;
	}
}
