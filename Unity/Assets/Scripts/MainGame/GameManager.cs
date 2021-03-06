﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
	private GameObject goBackground;
	private GameObject goAttach;
	private GameObject stageChildTemplate;
	private GameObject orbitTemplate;
	private GameObject panel;
	private ScrollManager scrollManager;
    
	private const float SCREEN_WIDTH = 1136.0f;
	private const float ORBIT_DROP_INTERVAL = 10.0F / 60.0F;
	private const float END_ANIMATION_INTERVAL = 1.0F;
	private const float START_ANIMATION_INTERVAL = 2.5F;
	private const string PANEL_PATH = "UI Root/Camera/Panel";
	private const string STAGE_PARENT_PATH = "UI Root/Camera/Panel/GOD_StageParent";
	private const string ATTACH_PATH = "UI Root/Camera/Panel/GOD_StageParent/GOD_Attach";
	private const string SCORE_PATH = "UI Root/Camera/Panel/GOD_GameMenu/GOD_Score/TXT_Score";
	private const string STAGE_CHILD_PREFAB_PATH = "Prefabs/MainGame/Stage/GOD_02StageChild";
	private const string STAGE_END_PREFAB_PATH = "Prefabs/MainGame/Stage/GOD_03StageEnd";
	private const string RESULT_PREFAB_PATH = "Prefabs/MainGame/Result/GOD_Result";
	private const string ORBIT_PREFAB_PATH = "Prefabs/MainGame/Chara/GOD_Orbit";
	private readonly Vector3 PLAYER1_DEFAULT_POSITION = new Vector3(-400.0F, 60.0F, 0.0F);
	private readonly Vector3 PLAYER2_DEFAULT_POSITION = new Vector3(-180.0F, -120.0F, 0.0F);

	public float Score { get; private set; }
	public float Life { get; private set; }
	private float scrollDistance = SCREEN_WIDTH;
	private float totalDistance = 0;
	private float orbitTime = 0.0F;
	private float endAnimateTime = 0.0F;
	private float startAnimateTime = 0.0F;
	private bool gameOver = false;
	private bool gameStarted = false;

	// Use this for initialization
	void Start () {
		panel = GameObject.Find(PANEL_PATH);

		GameObject player1 = Global.playerData[0].CreatePlayer();
		GameObject.Find("UI Root/Camera/Panel/GOD_TouchCheck/BTN_Left").GetComponent<UIButtonMessage>().target = player1;
		player1.transform.parent = panel.transform;
		player1.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
		player1.transform.localPosition = PLAYER1_DEFAULT_POSITION;
		if (Global.playerCount == 1) {
			GameObject.Find("UI Root/Camera/Panel/GOD_TouchCheck/BTN_Right").GetComponent<UIButtonMessage>().target = player1;
		} else {
			GameObject player2 = Global.playerData[1].CreatePlayer();
			GameObject.Find("UI Root/Camera/Panel/GOD_TouchCheck/BTN_Right").GetComponent<UIButtonMessage>().target = player2;
			player2.transform.parent = panel.transform;
			player2.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
			player2.transform.localPosition = PLAYER2_DEFAULT_POSITION;
		}

		goBackground = GameObject.Find(STAGE_PARENT_PATH);
		goAttach = GameObject.Find(ATTACH_PATH);
        
        scrollManager = GameObject.Find("GameScript").GetComponent<ScrollManager>();
		stageChildTemplate = (GameObject)Resources.Load(STAGE_CHILD_PREFAB_PATH);
		orbitTemplate = (GameObject)Resources.Load(ORBIT_PREFAB_PATH);

		this.Life = 1;
	}
	
	// Update is called once per frame
	void Update () {
		PauseState pauseState = GameObject.Find("Pause").GetComponent<PauseState>();

		if (gameOver && endAnimateTime <= END_ANIMATION_INTERVAL) {
			float diff = (SCREEN_WIDTH * 2 - scrollDistance) / (END_ANIMATION_INTERVAL - endAnimateTime + 1.0F / 60.0F) * Time.deltaTime;
			scrollAll(diff);
			endAnimateTime += Time.deltaTime;
			return;
		} else if (!gameStarted) {
			scrollAll(-SCREEN_WIDTH);
			gameStarted = true;
        }
		if (startAnimateTime <= START_ANIMATION_INTERVAL) {
			scrollAll(SCREEN_WIDTH * System.Math.Min(Time.deltaTime, START_ANIMATION_INTERVAL - startAnimateTime) / START_ANIMATION_INTERVAL);
			startAnimateTime += Time.deltaTime;
			return;
		}

		if (pauseState.paused || gameOver) {
			pauseState.Pause();
			return;
		}

		//  scroll
		float speed = scrollManager.scrollSpeed * Time.deltaTime;
		scrollDistance += speed;
		totalDistance += speed;
		goBackground.transform.localPosition += new Vector3(-speed, 0, 0);
		
		if (scrollDistance >= SCREEN_WIDTH) {
			scrollDistance -= SCREEN_WIDTH;
			addStage(stageChildTemplate);
        }
        
		orbitTime += Time.deltaTime;
		if (orbitTime >= ORBIT_DROP_INTERVAL) {
			UnityEngine.Object.FindObjectsOfType<Player>()
							  .ToList()
							  .ForEach(p => 
									{
										 GameObject orbit = (GameObject)Instantiate(orbitTemplate);
										 orbit.transform.parent = goBackground.transform;
										 orbit.transform.localPosition = new Vector2(totalDistance, 0.0F) + (Vector2)p.gameObject.transform.localPosition;
										 orbit.transform.localScale = new Vector3(1f, 1f, 1f);
										 GameObject cco = orbit.FindInChildWithTag("ColorChangeObject");
										 if (cco != null) {
											 cco.GetComponent<UISprite>().color = p.color;
										  }
							  		});
			orbitTime = 0.0F;
		}

		// score
		Score += Time.deltaTime * GameObject.Find("GameScript").GetComponent<ScrollManager>().scrollSpeed / 150;
		GameObject.Find(SCORE_PATH).GetComponent<UILabel>().text = string.Format("{0:f3}", Score);

		// life
		Life -= 0.015f * Time.deltaTime;

		//  dead
		if (Life < 0) {
			pauseState.Pause();
			gameOver = true;
			addStage((GameObject)Resources.Load(STAGE_END_PREFAB_PATH));

			GameObject result = (GameObject)Instantiate(Resources.Load(RESULT_PREFAB_PATH));
			result.transform.parent = panel.transform;
			result.transform.localScale = new Vector3(1f, 1f, 1f);
			GameObject.FindGameObjectWithTag("Score").GetComponent<UILabel>().text = string.Format("{0:f3}メェ〜とる", Score);;
		}
	}

	public void LifeUp() {
		Life = Mathf.Min(1.0f, this.Life + 0.1f);
		Debug.Log("Life: " + Life);
    }

	public void LifeUpStrong() {
		Life = Mathf.Min(1.0f, this.Life + 0.3f);
		Debug.Log("Life: " + Life);
	}

	public void Damage() {
		Life -= 0.1f;
		Debug.Log("Life: " + Life);
    }

	public void DamageStrong() {
		Life -= 0.3f;
		Debug.Log("Life: " + Life);
	}

	private void scrollAll(float value) {
		goBackground.transform.localPosition += new Vector3(-value, 0, 0);
		scrollDistance += value;
		UnityEngine.Object.FindObjectsOfType<Player>()
				   .Cast<Behaviour>()
				   .Concat(UnityEngine.Object.FindObjectsOfType<Item>().Cast<Behaviour>())
				   .ToList()
				   .ForEach(p => p.transform.localPosition += new Vector3(-value, 0.0F, 0.0F));
	}

	private void addStage(GameObject template) {	
		GameObject goStageChild = (GameObject)Instantiate(template);
		goStageChild.transform.parent = goAttach.transform;
		goStageChild.transform.localScale = new Vector3(1f, 1f, 1f);
		goAttach.GetComponent<UIGrid>().Reposition();
	}
}
