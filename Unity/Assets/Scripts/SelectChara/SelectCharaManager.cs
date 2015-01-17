using UnityEngine;
using System.Collections;

public class SelectCharaManager : MonoBehaviour {
	public int playerCount;

	// Use this for initialization
	void Start () {
		Global.playerCount = this.playerCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
