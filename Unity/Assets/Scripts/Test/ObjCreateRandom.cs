using UnityEngine;
using System.Collections;

public class ObjCreateRandom : MonoBehaviour {
	private GameObject go;
	private GameObject goCreated;
	private GameObject goParent;

	private float intervalTime;

	private const string GO_OBSTACLE = "Prefabs/Test/GOD_Obstacle";
	private const string GO_PARENT = "UI Root/Camera/Panel";

	// Use this for initialization
	void Start () {
		go = (GameObject)Resources.Load (GO_OBSTACLE);
		goParent = GameObject.Find (GO_PARENT);
	}
	
	// Update is called once per frame
	void Update () {
		intervalTime += Time.deltaTime;

		if (intervalTime >= 2.0f) {
			goCreated = (GameObject) Instantiate (go);
			goCreated.transform.parent = goParent.transform;

			intervalTime = 0.0f;
		}
	}
}