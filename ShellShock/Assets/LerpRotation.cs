using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotation : MonoBehaviour {

	public Transform from;
	public float speed = 0.1F;
	[SerializeField] private int offset = 0;
	Vector3 origin = new Vector3(-100, 0, 0);
	float t;

	void Awake () {
		t = Time.time;
		if (from == null) {
			from = this.transform;
		}
	}

	void Update() {
		Vector2 difference = (from.position - origin);
		difference.Normalize ();

		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Lerp(from.rotation, Quaternion.Euler (0f, 0f, rotZ + offset), (Time.time - t) * speed);
	}
}
