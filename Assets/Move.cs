using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(new Vector3(0.1f, 0, 0));
		GetComponent<Rigidbody> ().AddForce (200f, 0, 0);
	}
}
