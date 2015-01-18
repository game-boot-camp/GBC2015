using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SelectCharaManager : MonoBehaviour {
	public int playerCount;

	private GameObject[] player1Characters;
	private GameObject[] player2Characters;

	private const int PLAYER_TYPE_COUNT = 6;
	private const string CHARACTER_PREFAB_PATH = "Prefabs/MainGame/CharaSelect/GOD_Chara";
	private const string ATTACH_PATH = "UI Root/Camera/Panel/GOD_SelectChara/GOD_SelectPlayer{0}/GOD_Attach";

	// Use this for initialization
	void Start () {
		Global.playerCount = this.playerCount;

		player1Characters = createCharacters(1);
		if (playerCount > 1) {
			player2Characters = createCharacters(2);
		}
	}
	
	void ChangePlayer1TypeNext() {
		Global.playerData[0].CharacterType = (Global.playerData[0].CharacterType + 1) % PLAYER_TYPE_COUNT;
		showObjectSolo(player1Characters, Global.playerData[0].CharacterType);
	}
	
	void ChangePlayer1TypePrev() {
		Global.playerData[0].CharacterType = (Global.playerData[0].CharacterType + PLAYER_TYPE_COUNT - 1) % PLAYER_TYPE_COUNT;
		showObjectSolo(player1Characters, Global.playerData[0].CharacterType);
	}
	
	void ChangePlayer1Color1() {
		Global.playerData[0].color = Global.PlayerData.CharacterColor.Color1;
		applyColor(player1Characters, Color.white);
	}

	void ChangePlayer1Color2() {
		Global.playerData[0].color = Global.PlayerData.CharacterColor.Color2;
		applyColor(player1Characters, new Color(240f/255, 141f/255, 213f/255));
	}
	
	void ChangePlayer1Color3() {
		Global.playerData[0].color = Global.PlayerData.CharacterColor.Color3;
		applyColor(player1Characters, new Color(97f/255, 232f/255, 244f/255););
	}
	
	void ChangePlayer2TypeNext() {
		Global.playerData[1].CharacterType = (Global.playerData[1].CharacterType + 1) % PLAYER_TYPE_COUNT;
		showObjectSolo(player2Characters, Global.playerData[1].CharacterType);
	}
	
	void ChangePlayer2TypePrev() {
		Global.playerData[1].CharacterType = (Global.playerData[1].CharacterType + PLAYER_TYPE_COUNT - 1) % PLAYER_TYPE_COUNT;
		showObjectSolo(player2Characters, Global.playerData[1].CharacterType);
	}
	
	void ChangePlayer2Color1() {
		Global.playerData[1].color = Global.PlayerData.CharacterColor.Color1;
		applyColor(player2Characters, Color.white);
	}
	
	void ChangePlayer2Color2() {
		Global.playerData[1].color = Global.PlayerData.CharacterColor.Color2;
		applyColor(player2Characters, new Color(240f/255, 141f/255, 213f/255));
	}
	
	void ChangePlayer2Color3() {
		Global.playerData[1].color = Global.PlayerData.CharacterColor.Color3;
		applyColor(player2Characters, new Color(97f/255, 232f/255, 244f/255););
	}

	private GameObject[] createCharacters(int playerNumber) {
		var objects = Enumerable.Range(1, PLAYER_TYPE_COUNT)
								.Select(i => CHARACTER_PREFAB_PATH + i.ToString("d2"))
								.Select(i => (GameObject)Resources.Load(i))
								.Select(t => (GameObject)Instantiate(t))
								.ToArray();

		Transform parent = GameObject.Find(string.Format(ATTACH_PATH, playerNumber.ToString("d2"))).transform;
		foreach(var o in objects) {
			o.transform.parent = parent;
			o.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
			o.transform.localPosition = new Vector3();
		}

		showObjectSolo(objects, 0);

		return objects;
	}

	private void showObjectSolo(GameObject[] objects, int showObjectIndex) {
		objects.Where((o, i) => i != showObjectIndex).ToList().ForEach(o => o.SetActive(false));
		objects[showObjectIndex].SetActive(true);
	}

	private void applyColor(GameObject[] objects, Color color) {
		objects.SelectMany(o => o.FindInChildrenWithTag("ColorChangeObject"))
			   .Where(g => g != null)
			   .Select(g => g.GetComponent<UISprite>())
			   .ToList()
			   .ForEach(s => s.color = color);
	}
}
