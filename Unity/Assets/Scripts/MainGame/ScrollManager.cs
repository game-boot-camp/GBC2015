using UnityEngine;
using System.Collections;

public class ScrollManager : MonoBehaviour {
	public float scrollSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		scrollSpeed += 0.01f;
	}

	public void SpeedUp() {
		scrollSpeed *= 1.25f;
	}

	public void SpeedDown() {
		if (scrollSpeed > 0.5f) {
			scrollSpeed /= 1.2f;
		}
	}
}
