using UnityEngine;
using System.Collections;

public class CreateDamageObject : MonoBehaviour {
	public GameObject goDamage;

	private GameObject goParent;
	private const string GO_PARENT = "UI Root/Camera/Panel";

	private float intervalTimeThreshold = 8.0f;
	private float intervalTime;

	// Use this for initialization
	void Start () {
		goParent = GameObject.Find (GO_PARENT);
	}
	
	// Update is called once per frame
	void Update () {
		intervalTime += Time.deltaTime;
		
		if (intervalTime >= intervalTimeThreshold) {
			int index = Random.Range(0, 10);
	        GameObject goCreated = (GameObject)Instantiate(goDamage);
			goCreated.transform.parent = goParent.transform;
			goCreated.transform.localScale = new Vector3(1f, 1f, 1f);
    	    goCreated.transform.localPosition = new Vector3(568, -236 + index * 40, 0);

			intervalTime = 0.0f;
			if (intervalTimeThreshold > 5f) {
				intervalTimeThreshold -= 0.101f;
			}
		}

	}

}
