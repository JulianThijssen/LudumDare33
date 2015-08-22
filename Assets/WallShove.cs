using UnityEngine;
using System.Collections;

public class WallShove : MonoBehaviour {
	public float width = 5;
	public float force = 1000;

	private bool started = false;
	private float time = 0;

	private float leftPos, rightPos;

	// Use this for initialization
	void Start () {
		leftPos = transform.position.x + width;
		rightPos = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (!started) {
			return;
		}
		time += Time.deltaTime;

		if (transform.position.x < leftPos) {
			//transform.position = new Vector3 (x, transform.position.y, transform.position.z);
			GetComponent<Rigidbody> ().AddForce (force, 0, 0);
		} else {
			started = false;
			GetComponent<Rigidbody> ().velocity = new Vector3(0, 0, 0);
		}
	}

	public void OnAction () {
		started = true;
		time = 0;
	}
}
