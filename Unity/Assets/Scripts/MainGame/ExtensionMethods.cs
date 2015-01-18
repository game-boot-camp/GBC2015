using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods {
	public static GameObject FindInChildWithTag(this GameObject gameObject, string tag) {
		return gameObject.GetComponentsInChildren<Transform>(true)
						 .Where(t => t.tag == tag)
						 .Select(t => t.gameObject)
						 .FirstOrDefault();
	}

	public static GameObject[] FindInChildrenWithTag(this GameObject gameObject, string tag) {
		return gameObject.GetComponentsInChildren<Transform>(true)
						 .Where(t => t.tag == tag)
						 .Select(t => t.gameObject)
						 .ToArray();
	}

	public static GameObject FindByName(this GameObject gameObject, string name) {
		return gameObject.GetComponentsInChildren<Transform>(true)
						.Where(t => t.gameObject.name == name)
				.Select(t => t.gameObject)
				.FirstOrDefault();
	}
}