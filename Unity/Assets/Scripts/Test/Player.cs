using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private Vector3 speed = new Vector3(0, 2f, 0);

	// Use this for initialization
	void Start () {
	
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
			break;
		case Item.ItemType.LifeUp:
			message = "LifeUp";
			break;
		case Item.ItemType.SpeedDown:
			message = "SpeedDown";
			break;
		case Item.ItemType.SpeedUp:
			message = "SpeedUp";
            break;
        }
        Debug.Log(message + " " + this.gameObject.name + "←" + go.gameObject.name);
	}
}