using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	public enum TrapType{WallShover, Cannon};

	public GameObject target;
	public TrapType trapType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider) {
		if (!(collider.gameObject.tag == "Player")) {
			return;
		}

		if (trapType == TrapType.WallShover) {
			target.GetComponent<WallShove>().OnAction();
		}
		if (trapType == TrapType.Cannon) {
			target.GetComponent<Cannon>().OnAction();
		}
	}
}
