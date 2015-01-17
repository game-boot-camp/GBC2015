using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject goBackground;
	private GameObject goAttach;
	private ScrollManager scrollManager;
    
	private const float SCREEN_WIDTH = 1136;

	public float Score { get; private set; }
	public float Life { get; private set; }
	private float scrollDistance = SCREEN_WIDTH;

	// Use this for initialization
	void Start () {
		goBackground = GameObject.Find("UI Root/Camera/Panel/GOD_StageParent");
		goAttach = GameObject.Find("UI Root/Camera/Panel/GOD_StageParent/GOD_Attach");
        
        scrollManager = GameObject.Find("GameScript").GetComponent<ScrollManager>();

		this.Life = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//  scroll
		float speed = scrollManager.scrollSpeed;
		scrollDistance += speed;
		goBackground.transform.localPosition += new Vector3(-speed, 0, 0);
		
		if (scrollDistance >= SCREEN_WIDTH) {
			scrollDistance -= SCREEN_WIDTH;
			GameObject goStageChild = (GameObject)Instantiate(Resources.Load("Prefabs/MainGame/Stage/GOD_StageChild"));
			goStageChild.transform.parent = goAttach.transform;
			goStageChild.transform.localScale = new Vector3(1f, 1f, 1f);
            goAttach.GetComponent<UIGrid>().Reposition();
        }
        
        
        PauseState pauseState = GameObject.Find("Pause").GetComponent<PauseState>();
		if (pauseState.paused) { return; }

		// score
		Score += Time.deltaTime;
		GameObject.Find("UI Root/Camera/Panel/GOD_GameMenu/GOD_Score/TXT_Score").GetComponent<UILabel>().text = string.Format("{0:f3}m", Score);

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
