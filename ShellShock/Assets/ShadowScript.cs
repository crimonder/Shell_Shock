using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour {

	public GameObject shadowObject;

	void Awake () {
		Vector3 tmp = new Vector3 (transform.position.x - Spawner.spawner.x, transform.position.y - Spawner.spawner.y, transform.position.z);
		GameObject shadow = Instantiate (shadowObject, tmp, this.transform.rotation) as GameObject;
		shadow.transform.SetParent (this.transform);
		shadow.transform.localScale = Vector3.one;
	}
}
