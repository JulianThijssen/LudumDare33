using UnityEngine;
using System.Collections;

public class SpikeField : MonoBehaviour {
	public float period = 2f;
	public float spike = 0.5f;

	private float time;

	private float startHeight;
	private float endHeight;

	// Use this for initialization
	void Start () {
		startHeight = transform.position.y;
		endHeight = transform.position.y + 1;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		float x = transform.position.x;
		float y = 0;
		float z = transform.position.z;

		if (time <= spike) {
			y = Mathf.Lerp (startHeight, endHeight, time / spike);
		}
		if (time > spike) {
			y = Mathf.Lerp (endHeight, startHeight, (time - spike) / (period - spike));
		}
		if (time >= period) {
			time = 0;
		}

		transform.position = new Vector3 (x, y, z);
	}
}
