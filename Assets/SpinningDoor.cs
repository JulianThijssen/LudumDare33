using UnityEngine;
using System.Collections;

public class SpinningDoor : MonoBehaviour {
	public float rotationSpeed = 2f;

	private GameObject door;

	// Use this for initialization
	void Start () {
		door = transform.Find ("Door").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		door.transform.Rotate (new Vector3 (0, rotationSpeed, 0));
	}
}
