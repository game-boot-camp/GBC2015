using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public enum ItemType {
		Damage,
		LifeUp,
		SpeedUp,
		SpeedDown
	}

	private const float SPEED = 2.0f;
	
	public ItemType type;
	
	void Start() {
		this.transform.localPosition = new Vector3(568, Random.Range(-320, 320), 0);
		this.transform.localScale = new Vector3(1f, 1f, 1f);
	}
	
	void Update() {
		this.gameObject.transform.localPosition += new Vector3 (-SPEED, 0, 0);
		
		if (this.gameObject.transform.localPosition.x <= -600f) {
			GameObject.Destroy(this.gameObject);
		}
	}
	
    void OnTriggerEnter2D() {
        GameObject.Destroy (this.gameObject);
    }
}