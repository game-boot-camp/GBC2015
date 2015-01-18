using UnityEngine;
using System.Collections;

public class ScrollManager : MonoBehaviour {
	public float scrollSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		scrollSpeed += 0.015f;
	}

	public void SpeedUp() {
		scrollSpeed *= 1.25f;
	}

	public void SpeedDown() {
		if (scrollSpeed > 100.0f) {
			scrollSpeed /= 1.2f;
		}
	}
}
