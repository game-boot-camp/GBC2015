using UnityEngine;
using System.Collections;
using System.Linq;

public class Global {
	public static int playerCount;
	public static PlayerData[] playerData = new PlayerData[2];

	static Global() {
		playerData[0] = new PlayerData();
		playerData[1] = new PlayerData();
	}

	public class PlayerData {
		public enum CharacterColor {
			Color1,
			Color2,
			Color3
		}

		public CharacterColor color { get; set; }
		public int CharacterType { get; set; }

		private const string CHARA_PREFAB_PATH = "Prefabs/MainGame/Chara/GOD_Chara";
		private const string DAMAGED_PREFAB_PATH = "Prefabs/MainGame/CharaSelect/GOD_Chara";

		public GameObject CreatePlayer() {
			var resource = Resources.Load(CHARA_PREFAB_PATH + (CharacterType + 1).ToString("d2"));
			GameObject obj = (GameObject)Object.Instantiate(resource);
			Player player = obj.GetComponent<Player>();
			switch(color) {
			case CharacterColor.Color1:
				player.color = Color.white;
				break;
			case CharacterColor.Color2:
				player.color = new Color(240f/255, 141f/255, 213f/255);;
				break;
			case CharacterColor.Color3:
				player.color = new Color(97f/255, 232f/255, 244f/255);
				break;
			}
			return obj;
		}

		public GameObject CreateDamagedSheep() {
			var resource = Resources.Load(DAMAGED_PREFAB_PATH + (CharacterType + 1).ToString("d2"));
			GameObject obj = (GameObject)Object.Instantiate(resource);
			Color c = new Color();
			switch(color) {
			case CharacterColor.Color1:
				c = Color.white;
				break;
			case CharacterColor.Color2:
				c = new Color(240f/255, 141f/255, 213f/255);;
				break;
			case CharacterColor.Color3:
				c = new Color(97f/255, 232f/255, 244f/255);
				break;
			}

			obj.FindInChildrenWithTag("ColorChangeObject")
			   .Where(g => g != null)
			   .Select(g => g.GetComponent<UISprite>())
			   .ToList()
			   .ForEach(s => s.color = c);

			UISprite bodySprite = obj.FindInChildrenWithTag("ColorChangeObject").Where(b => b.name.EndsWith("Body")).First().GetComponent<UISprite>();
			UISprite faceSprite = obj.GetComponentsInChildren<UISprite>().Where(b => b.name.EndsWith("Face")).First().GetComponent<UISprite>();
			bodySprite.enabled = false;
			faceSprite.spriteName = "Sheep_Face02";

			return obj;
		}
	}
}
