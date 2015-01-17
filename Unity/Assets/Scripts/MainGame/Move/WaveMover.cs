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
		gameObject.transform.localPosition += new Vector3(-100.0f * Time.deltaTime, Mathf.Sin(time * 4.0f) * 300.0f * Time.deltaTime, 0.0f);
	}
}
