using UnityEngine;
using System.Collections;
using System.Linq;

public class CreateItems : MonoBehaviour {
	private GameObject[] goItems;
	private GameObject goCreated;
	private GameObject goParent;

	private float intervalTime;

	private readonly string[] GO_ITEM_PATHS = new[] {
		"Prefabs/MainGame/Item/GOD_ItemDamageStrong",
        "Prefabs/MainGame/Item/GOD_ItemLifeUp",
		"Prefabs/MainGame/Item/GOD_ItemLifeUpStrong",
		"Prefabs/MainGame/Item/GOD_ItemSpeedDown",
		"Prefabs/MainGame/Item/GOD_ItemSpeedUp",
		"Prefabs/MainGame/Item/GOD_ItemAngleUp",
        "Prefabs/MainGame/Item/GOD_ItemChangePosition",
		"Prefabs/MainGame/Item/GOD_ItemSpecialColor",
		"Prefabs/MainGame/Item/GOD_ItemSpecialShape",
    };
	private int[][] incidence = new int[2][];
	private int incident_sum = 0;

    private const string GO_PARENT = "UI Root/Camera/Panel";

	CreateItems () {
		incidence[0] = new int[]{ 5, 50, 10, 20, 15, 10, 0, 10, 0 };
		incidence[1] = new int[]{ 5, 50, 10, 20, 15, 10, 15, 20, 0 };
	}

	// Use this for initialization
	void Start () {
		goItems = GO_ITEM_PATHS.Select(p => Resources.Load(p)).Cast<GameObject>().ToArray();
		goParent = GameObject.Find (GO_PARENT);

		foreach (int incident in incidence[Global.playerCount==1 ? 0 : 1]) {
			incident_sum += incident;
		}
	}
	
	// Update is called once per frame
	void Update () {
		intervalTime += Time.deltaTime;

		if (intervalTime >= 2.0f) {
			int rand = Random.Range(0, incident_sum);
			int sum = 0;
			int[] incidence = this.incidence[Global.playerCount== 1 ? 0 : 1];
			for (int i=0; i<=incidence.Length; i++) {
				sum += incidence[i];
				if (sum >= rand) {
					goCreated = (GameObject)Instantiate(goItems[i]);
					goCreated.transform.parent = goParent.transform;
					break;
				}
			}

			intervalTime = 0.0f;
		}
	}
}