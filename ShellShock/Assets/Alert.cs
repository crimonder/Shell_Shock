using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour {
	
	float rad;
	Vector3 origin = new Vector3(-100, 0, 0);

	void Awake () {
//		rad = Mathf.Atan2 (transform.position.y - origin.y, transform.position.x - origin.x);
//		Vector3 thisVector3 = transform.position;
//		float x = thisVector3.x, y = thisVector3.y;
//		if (thisVector3.x > -68.5f) {
//			y = y - (x + 68) * Mathf.Tan (rad);
//			x = -68f;
//		} else if (thisVector3.x < -132.5f) {
//			y = y - (-128 - x) * Mathf.Tan (rad);
//			x = -132f;
//		}
//		if (thisVector3.y > 24.5f) {
//			x = x - (y - 24) / Mathf.Tan (rad);
//			y = 24f;
//		} else if (thisVector3.y < -24.5f) {
//			x = x - (y + 24) / Mathf.Tan (rad);
//			y = -24f;
//		}
//		transform.position = new Vector3(x, y, 0);
	}
}
