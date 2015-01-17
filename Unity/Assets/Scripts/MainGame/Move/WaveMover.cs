using UnityEngine;
using System.Collections;

public class WaveMover : MonoBehaviour {
	private float time = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PauseState pauseState = GameObject.Find("Pause").GetComponent<PauseState>();
		if (pauseState.paused) { return; }

		time += Time.deltaTime;
		gameObject.transform.localPosition += new Vector3(-1.0f, Mathf.Sin(time * 4.0f) * 3.0f, 0.0f);
	}
}
