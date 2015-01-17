using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods {
	public static GameObject FindInChildrenWithTag(this GameObject gameObject, string tag) {
		return gameObject.GetComponentsInChildren<Transform>(true)
						 .Where(t => t.tag == tag)
						 .Select(t => t.gameObject)
						 .FirstOrDefault();
	}
}