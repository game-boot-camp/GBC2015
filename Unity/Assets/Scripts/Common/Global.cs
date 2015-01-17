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
		public enum CharacterColor { }
	}
}
