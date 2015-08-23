using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	public GameObject cannonBall;
	public float period = 3f;
	public float force = 200000f;

	private float time = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time > period) {
			GameObject ball = (GameObject) Instantiate (cannonBall, transform.Find ("SpawnCannonball").position, Quaternion.identity);
			ball.GetComponent<Rigidbody>().AddForce (-transform.forward * force);
			time = 0;
		}
	}
}
