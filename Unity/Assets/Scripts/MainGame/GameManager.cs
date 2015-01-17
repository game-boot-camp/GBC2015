using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject goBackground;
	private GameObject goAttach;
	private ScrollManager scrollManager;
    
	private const float SCREEN_WIDTH = 1136;

	public int Score { get; set; }
	private float scrollDistance = SCREEN_WIDTH;

	// Use this for initialization
	void Start () {
		goBackground = GameObject.Find("UI Root/Camera/Panel/GOD_StageParent");
		goAttach = GameObject.Find("UI Root/Camera/Panel/GOD_StageParent/GOD_Attach");
        
        scrollManager = GameObject.Find("GameScript").GetComponent<ScrollManager>();
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
	}
}
