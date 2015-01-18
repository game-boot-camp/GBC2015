using UnityEngine;
using System.Collections;

public class TransitionManager : MonoBehaviour {

	private GameObject goPanel;
	private GameObject goFadeTamplate;
	private GameObject goFade;

	private const string PANEL_PATH = "UI Root/Camera/Panel";
	private const string FADE_PATH = "Prefabs/Common/GOD_Loading";
	private const string FADE_ANIM_NAME = "Anim_Loading";

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		goPanel = GameObject.Find (PANEL_PATH);
		goFadeTamplate = (GameObject)Resources.Load (FADE_PATH);
		goFade = (GameObject)Instantiate(goFadeTamplate);
		
		goFade.transform.parent = goPanel.transform;
		goFade.transform.localScale = new Vector3 (1.0f,1.0f, 1.0f);

		goFade.animation.Play (FADE_ANIM_NAME);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
