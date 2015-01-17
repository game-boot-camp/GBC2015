using UnityEngine;
using System.Collections;
using System.Linq;

public class ObjCreateRandom : MonoBehaviour {
	private GameObject[] goItems;
	private GameObject goCreated;
	private GameObject goParent;

	private float intervalTime;

	private readonly string[] GO_ITEM_PATHS = new[] {
		"Prefabs/MainGame/Item/GOD_ItemDamage",
		"Prefabs/MainGame/Item/GOD_ItemLifeUp",
		"Prefabs/MainGame/Item/GOD_ItemSpeedDown",
		"Prefabs/MainGame/Item/GOD_ItemSpeedUp"
	};
	private const string GO_PARENT = "UI Root/Camera/Panel";

	// Use this for initialization
	void Start () {
		goItems = GO_ITEM_PATHS.Select(p => Resources.Load(p)).Cast<GameObject>().ToArray();
		goParent = GameObject.Find (GO_PARENT);
	}
	
	// Update is called once per frame
	void Update () {
		intervalTime += Time.deltaTime;

		if (intervalTime >= 2.0f) {
			goCreated = (GameObject)Instantiate(goItems[Random.Range(0, goItems.Length)]);
			goCreated.transform.parent = goParent.transform;

			intervalTime = 0.0f;
		}
	}
}