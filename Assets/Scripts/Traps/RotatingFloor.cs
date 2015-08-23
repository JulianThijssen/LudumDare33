using UnityEngine;
using System.Collections;

public class RotatingFloor : MonoBehaviour {
	public float speed = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, speed, 0));
	}

	void OnCollisionStay(Collision col) {
		if (!(col.gameObject.tag == "Player")) {
			return;
		}
		//Debug.Log ("Collision!");
		GameObject collider = col.gameObject;
		Vector3 p = col.transform.position;
		Vector3 c = transform.position;
		Vector2 v = new Vector2 (p.x - c.x, p.z - c.z);
		v.Normalize ();
		Vector2 n = new Vector2 (v.y, -v.x);
		n *= 20 * v.magnitude;
		//Debug.Log (n);
		collider.GetComponent<Rigidbody> ().AddForce (new Vector3(n.x, 0, n.y), ForceMode.Impulse);
	}
}
