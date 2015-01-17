﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class CreateItems : MonoBehaviour {
	private GameObject[] goItems;
	private GameObject goCreated;
	private GameObject goParent;

	private float intervalTime;

	private readonly string[] GO_ITEM_PATHS = new[] {
		"Prefabs/MainGame/Item/GOD_ItemDamage",
		"Prefabs/MainGame/Item/GOD_ItemDamageStrong",
        "Prefabs/MainGame/Item/GOD_ItemLifeUp",
		"Prefabs/MainGame/Item/GOD_ItemLifeUpStrong",
		"Prefabs/MainGame/Item/GOD_ItemSpeedDown",
		"Prefabs/MainGame/Item/GOD_ItemSpeedUp",
//		"Prefabs/MainGame/Item/GOD_ItemAngleUp",
//        "Prefabs/MainGame/Item/GOD_ItemChangePosition",
		"Prefabs/MainGame/Item/GOD_ItemSpecialColor",
//		"Prefabs/MainGame/Item/GOD_ItemSpecialShape",
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