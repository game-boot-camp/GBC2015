using UnityEngine;
using System.Collections;

public class TransitionManager : MonoBehaviour {

	private GameObject goPanel;
	private GameObject goFade;

	private const string PANEL_PATH = "UI Root/Camera/Panel";
	private const string FADE_PATH = "Prefabs/Common/GOD_Loading";

	void Awake() {
		goPanel = GameObject.Find (PANEL_PATH);
		goFade = (GameObject)Resources.Load (FADE_PATH);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
