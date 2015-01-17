using UnityEngine;
using System.Collections;

public class ScrollManager : MonoBehaviour {
	public float scrollSpeed = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		scrollSpeed += 0.01f; // test
	}
}
