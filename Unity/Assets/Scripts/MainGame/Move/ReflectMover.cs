using UnityEngine;
using System.Collections;

public class ReflectMover : MonoBehaviour {
	private float speed;

	// Use this for initialization
	void Start () {
		speed = (Random.value - 0.5f) * 300;
	}
	
	// Update is called once per frame
	void Update () {
		PauseState pauseState = GameObject.Find("Pause").GetComponent<PauseState>();
		if (pauseState.paused) { return; }

		gameObject.transform.localPosition += new Vector3(-60f * Time.deltaTime, speed * Time.deltaTime, 0.0f);
		if ((this.transform.localPosition.y >= 170 && speed > 0f) || (this.transform.localPosition.y <= -270 && speed < 0f)) {
			speed = -speed;
		}
	}
}
