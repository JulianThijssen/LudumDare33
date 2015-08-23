using UnityEngine;
using System.Collections;

public class LockMouse : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.isPaused () || GameController.isDead()) {
			Cursor.lockState =  CursorLockMode.None;
			Cursor.visible = true;
		}
		else {
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
