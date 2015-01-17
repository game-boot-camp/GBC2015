using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public enum ItemType {
		Damage,
		LifeUp,
		SpeedUp,
		SpeedDown,
		ChangePosition,
	}

	public ItemType type;
	
	void Start() {
		this.transform.localPosition = new Vector3(568, Random.Range(-200, 200), 0);
		this.transform.localScale = new Vector3(1f, 1f, 1f);
	}
	
	void Update() {
		float speed = GameObject.Find("GameScript").GetComponent<ScrollManager>().scrollSpeed;

		this.gameObject.transform.localPosition += new Vector3 (-speed, 0, 0);
		
		if (this.gameObject.transform.localPosition.x <= -600f) {
			GameObject.Destroy(this.gameObject);
		}
	}
	
    void OnTriggerEnter2D() {
        GameObject.Destroy (this.gameObject);
    }
}