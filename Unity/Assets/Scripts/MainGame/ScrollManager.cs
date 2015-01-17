using UnityEngine;
using System.Collections;

public class ScrollManager : MonoBehaviour {
	public float scrollSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		scrollSpeed += 0.01f; // test
	}

	public void SpeedUp() {
		scrollSpeed *= 1.2f;
	}

	public void SpeedDown() {
		scrollSpeed /= 1.2f;
	}
}
