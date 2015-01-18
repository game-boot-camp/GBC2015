using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	private GameManager gameManager;
	private ScrollManager scrollManager;
	private GameObject[] body;

	private const float NORMAL_SPEED = 150.0f;
	private const float HIGH_SPEED = 500.0f;
	private const string SPRITE_NAME = "Sheep_Body";

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
		body = gameObject.FindInChildrenWithTag("ColorChangeObject");
		if (color == new Color()) {
			color = Color.white;
		}
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
		if (body != null) {
			body.Select(b => b.GetComponent<UISprite>()).ToList().ForEach(s => s.color = color);
			UISprite bodySprite = body.Where(b => b.name.EndsWith("Body")).First().GetComponent<UISprite>();
			UISprite faceSprite = this.GetComponentsInChildren<UISprite>().Where(b => b.name.EndsWith("Face")).First().GetComponent<UISprite>();
            if (gameManager.Life > 0.0) {
				bodySprite.spriteName = SPRITE_NAME + (5 - (int)System.Math.Ceiling(gameManager.Life / 0.25)).ToString("d2");
				faceSprite.spriteName = (gameManager.Life > 0.25) ? "Sheep_Face01" : "Sheep_Face02";
			} else {
				bodySprite.enabled = false;
				faceSprite.spriteName = "Sheep_Face02";
			}
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
			color = new Color(Random.Range(0.4F, 1.0F), Random.Range(0.4F, 1.0F), Random.Range(0.4F, 1.0F));
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
		Invoke ("ChangePlayer", 0.2f);
    }

	private void ChangePlayer() {
		UIButtonMessage button1 = GameObject.Find("UI Root/Camera/Panel/GOD_TouchCheck/BTN_Left").GetComponent<UIButtonMessage>();
		UIButtonMessage button2 = GameObject.Find("UI Root/Camera/Panel/GOD_TouchCheck/BTN_Right").GetComponent<UIButtonMessage>();
		
		GameObject tmp = button1.target;
		button1.target = button2.target;
        button2.target = tmp;
                
		Player player1 = null;
		Player player2 = null;
		foreach (Player player in GameObject.FindObjectsOfType(typeof(Player))) {
			if (player1 == null) { player1 = player; continue; }
			player2 = player;
		}

		if (player2 != null) {
			Color tmpColor = player1.color;
			player1.color = player2.color;
			player2.color = tmpColor;

			float tmpSpeed = player1.speed;
			player1.speed = player2.speed;
			player2.speed = tmpSpeed;

			float tmpHighSpeedTimeRest = player1.highSpeedTimeRest;
			player1.highSpeedTimeRest = player2.highSpeedTimeRest;
			player2.highSpeedTimeRest = tmpHighSpeedTimeRest;
		}
    }
}