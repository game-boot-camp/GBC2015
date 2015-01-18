using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private GameManager gameManager;
	private ScrollManager scrollManager;
	private GameObject colorChangeObject;

	private const float NORMAL_SPEED = 150.0f;
	private const float HIGH_SPEED = 500.0f;

	private int direction = 1;
	private float speed = NORMAL_SPEED;

	private float highSpeedTimeRest = 0.0f;

	public Color color { get; set; }


	private float time = 0.0f;

	// Use this for initialization
	void Start () {
		GameObject gameScript = GameObject.Find("GameScript");
		gameManager = gameScript.GetComponent<GameManager>();
		scrollManager = gameScript.GetComponent<ScrollManager>();
		colorChangeObject = gameObject.FindInChildrenWithTag("ColorChangeObject");
		color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		if ((time += Time.deltaTime) <= 2.5f) { return; } // XXX:

		ChangeDirectionIfNeeded();

		if (highSpeedTimeRest > 0.0f) {
			highSpeedTimeRest -= Time.deltaTime;
			if (highSpeedTimeRest < 0.0f) {
				highSpeedTimeRest = 0;
				speed = NORMAL_SPEED;
			}
		}
		if (colorChangeObject != null) {
			colorChangeObject.GetComponent<UISprite>().color = color;
		}

		this.transform.localPosition += new Vector3(0, speed * direction * Time.deltaTime, 0);
    }

	//　画面がタップをされた際の処理
	void TapAction() {
		direction = -direction;
		ChangeDirectionIfNeeded();
    }

	private void ChangeDirectionIfNeeded() {
		if (this.transform.localPosition.y >= 170) {
			direction = -1;
		} else if (this.transform.localPosition.y <= -270) {
			direction = 1;
        }
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
		case Item.ItemType.DamageStrong:
			gameManager.DamageStrong();
			break;
		case Item.ItemType.LifeUp:
			message = "LifeUp";
			gameManager.LifeUp();
			break;
		case Item.ItemType.LifeUpStrong:
			message = "LifeUpStrong";
			gameManager.LifeUpStrong();
            break;
		case Item.ItemType.SpeedDown:
			message = "SpeedDown";
			scrollManager.SpeedDown();
			break;
		case Item.ItemType.SpeedUp:
			message = "SpeedUp";
			scrollManager.SpeedUp ();
            break;
		case Item.ItemType.AngleUp:
			message = "AngleUp";
			ItemAngleUp();
            break;
		case Item.ItemType.ChangePosition:
			message = "ChangePosition";
			ItemChangePosition();
			break;
		case Item.ItemType.SpecialColor:
			message = "SpecialColor";
			color = new Color(Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F));
			break;
		case Item.ItemType.SpecialShape:
			message = "SpecialShape";
			break;
        }
        Debug.Log(message + " " + this.gameObject.name + "←" + go.gameObject.name);
	}

	private void ItemAngleUp() {
		speed = HIGH_SPEED;
		highSpeedTimeRest = 6.0f;
	}

	private void ItemChangePosition() {
		foreach (Player player in GameObject.FindObjectsOfType(typeof(Player))) {
			TweenRotation rotation = player.gameObject.AddComponent<TweenRotation>();
			rotation.from = new Vector3(0, 0, 0);
			rotation.to = new Vector3(0, 180, 0);
			rotation.duration = 0.2f;

			rotation = player.gameObject.AddComponent<TweenRotation>();
			rotation.from = new Vector3(0, 180, 0);
			rotation.to = new Vector3(0, 360, 0);
			rotation.duration = 0.2f;
			rotation.delay = 0.2f;
		}

		UIButtonMessage button1 = GameObject.Find("UI Root/Camera/Panel/GOD_TouchCheck/BTN_Left").GetComponent<UIButtonMessage>();
		UIButtonMessage button2 = GameObject.Find("UI Root/Camera/Panel/GOD_TouchCheck/BTN_Right").GetComponent<UIButtonMessage>();

		GameObject tmp = button1.target;
		button1.target = button2.target;
		button2.target = tmp;
	}
}