using UnityEngine;
using System.Collections;

public class CSceneTransition : MonoBehaviour {

	public string sceneName;

	void OnClick() {
		Application.LoadLevel (sceneName);
	}
}