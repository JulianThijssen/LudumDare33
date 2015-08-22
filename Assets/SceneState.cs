using UnityEngine;
using System.Collections;

public class SceneState : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadGame() {
		Debug.Log ("Help");
		Application.LoadLevel ("scene");
	}
}
