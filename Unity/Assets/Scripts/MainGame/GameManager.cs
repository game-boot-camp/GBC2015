using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
	private GameObject goBackground;
	private GameObject goAttach;
	private GameObject stageChildTemplate;
	private GameObject orbitTemplate;
	private ScrollManager scrollManager;
    
	private const float SCREEN_WIDTH = 1136.0f;
	private const string STAGE_PARENT_PATH = "UI Root/Camera/Panel/GOD_StageParent";
	private const string ATTACH_PATH = "UI Root/Camera/Panel/GOD_StageParent/GOD_Attach";
	private const string SCORE_PATH = "UI Root/Camera/Panel/GOD_GameMenu/GOD_Score/TXT_Score";
	private const string STAGE_CHILD_PREFAB_PATH = "Prefabs/MainGame/Stage/GOD_StageChild";
	private const string ORBIT_PREFAB_PATH = "Prefabs/MainGame/Chara/GOD_Orbit";

	public float Score { get; private set; }
	public float Life { get; private set; }
	private float scrollDistance = SCREEN_WIDTH;
	private float totalDistance = 0;
	private int orbitFrameCount = 0;

	// Use this for initialization
	void Start () {
		goBackground = GameObject.Find(STAGE_PARENT_PATH);
		goAttach = GameObject.Find(ATTACH_PATH);
        
        scrollManager = GameObject.Find("GameScript").GetComponent<ScrollManager>();
		stageChildTemplate = (GameObject)Resources.Load(STAGE_CHILD_PREFAB_PATH);
		orbitTemplate = (GameObject)Resources.Load(ORBIT_PREFAB_PATH);

		this.Life = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//  scroll
		float speed = scrollManager.scrollSpeed;
		scrollDistance += speed;
		totalDistance += speed;
		goBackground.transform.localPosition += new Vector3(-speed, 0, 0);
		
		if (scrollDistance >= SCREEN_WIDTH) {
			scrollDistance -= SCREEN_WIDTH;
			GameObject goStageChild = (GameObject)Instantiate(stageChildTemplate);
			goStageChild.transform.parent = goAttach.transform;
			goStageChild.transform.localScale = new Vector3(1f, 1f, 1f);
            goAttach.GetComponent<UIGrid>().Reposition();
        }
        
		orbitFrameCount++;
		if (orbitFrameCount >= 30) {
			UnityEngine.Object.FindObjectsOfType<Player>()
							  .Select(p => (Vector2)p.gameObject.transform.localPosition)
							  .ToList()
							  .ForEach(p => 
									{
										 GameObject orbit = (GameObject)Instantiate(orbitTemplate);
										 orbit.transform.parent = goBackground.transform;
										 orbit.transform.localPosition = new Vector2(totalDistance, 0.0F) + p;
										 orbit.transform.localScale = new Vector3(1f, 1f, 1f);
							  		});
			orbitFrameCount = 0;
		}
        
        PauseState pauseState = GameObject.Find("Pause").GetComponent<PauseState>();
		if (pauseState.paused) { return; }

		// score
		Score += Time.deltaTime;
		GameObject.Find(SCORE_PATH).GetComponent<UILabel>().text = string.Format("{0:f3}m", Score);

		// life
		Life -= 0.02f * Time.deltaTime;

		//  dead
		if (Life < 0) {
			pauseState.Pause();
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
		Life -= 0.2f;
		Debug.Log("Life: " + Life);
    }

	public void DamageStrong() {
		Life -= 0.4f;
		Debug.Log("Life: " + Life);
	}
}
