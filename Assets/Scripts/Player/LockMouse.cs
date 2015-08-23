using UnityEngine;
using System.Collections;

public class LockMouse : MonoBehaviour {
	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameController.isPaused () || GameController.isDead()) {
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
