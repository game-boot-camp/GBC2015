using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private GameManager gameManager;
	private ScrollManager scrollManager;

	private Vector3 speed = new Vector3(0, 2f, 0);

	// Use this for initialization
	void Start () {
		GameObject gameScript = GameObject.Find("GameScript");
		gameManager = gameScript.GetComponent<GameManager>();
		scrollManager = gameScript.GetComponent<ScrollManager>();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition += speed;

		if (this.transform.localPosition.y >= 200 || this.transform.localPosition.y <= -200)
			speed *= -1;	
	}

	//　画面がタップをされた際の処理
	void TapAction() {
		speed *= -1;
	}

	//　物体の衝突時の処理
	void OnTriggerEnter2D(Collider2D go) {
		Item item = (Item)go.gameObject.GetComponent(typeof(Item));
		string message = "";
		switch (item.type)
		{
		case Item.ItemType.Damage:
			message = "Damage";
			gameManager.Damage();
			break;
		case Item.ItemType.LifeUp:
			message = "LifeUp";
			gameManager.LifeUp();
			break;
		case Item.ItemType.SpeedDown:
			message = "SpeedDown";
			scrollManager.SpeedDown();
			break;
		case Item.ItemType.SpeedUp:
			message = "SpeedUp";
			scrollManager.SpeedUp ();
            break;
		case Item.ItemType.ChangePosition:
			message = "ChangePosition";
			ItemChangePosition();
			break;
        }
        Debug.Log(message + " " + this.gameObject.name + "←" + go.gameObject.name);
	}

	private void ItemChangePosition() {
		UIButtonMessage button1 = GameObject.Find("UI Root/Camera/Panel/GOD_TouchCheck/BTN_Left").GetComponent<UIButtonMessage>();
		UIButtonMessage button2 = GameObject.Find("UI Root/Camera/Panel/GOD_TouchCheck/BTN_Right").GetComponent<UIButtonMessage>();

		GameObject tmp = button1.target;
		button1.target = button2.target;
		button2.target = tmp;
	}
}