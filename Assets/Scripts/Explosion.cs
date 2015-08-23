using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public float radius = 10f;
	public float explosionTime = 2f;
	
	private float time = 0;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, explosionTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (time <= explosionTime) {
			time += Time.deltaTime;

			float r = Mathf.Lerp (0, radius, time / explosionTime);
			transform.localScale = new Vector3(r, r, r);
		}
	}
}
