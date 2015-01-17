using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.localPosition = new Vector3(568, Random.Range(-320, 320), 0);
		this.transform.localScale = new Vector3(1f, 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		float speed = GameObject.Find("GameScript").GetComponent<ScrollManager>().scrollSpeed;

		this.gameObject.transform.localPosition += new Vector3 (-speed, 0, 0);

		if (this.gameObject.transform.localPosition.x <= -600f) {
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D() {
		GameObject.Destroy (this.gameObject);
	}
}