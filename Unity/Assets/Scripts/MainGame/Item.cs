using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public enum ItemType {
		Damage,
		DamageStrong,
		LifeUp,
		LifeUpStrong,
		SpeedUp,
		SpeedDown,
		AngleUp,
		ChangePosition,
		SpecialColor,
		SpecialShape,
	}

	public ItemType type;
	
	void Start() {
		this.transform.localPosition = new Vector3(568, Random.Range(-200, 200), 0);
		this.transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x), 1f, 1f);
	}
	
	void Update() {
		float speed = GameObject.Find("GameScript").GetComponent<ScrollManager>().scrollSpeed * Time.deltaTime;

		this.gameObject.transform.localPosition += new Vector3 (-speed, 0, 0);
		
		if (this.gameObject.transform.localPosition.x <= -600f) {
			GameObject.Destroy(this.gameObject);
		}
	}

    void OnTriggerEnter2D(Collider2D go) {
		if (go.GetComponent<Player>() != null) {
        	GameObject.Destroy (this.gameObject);
		}
    }
}