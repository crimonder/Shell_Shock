﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

	public float amount;
	public GameObject alert;
	Vector3 origin = new Vector3(-100, 0, 0);
	public int score;
	float rad;
	public Vector2 thisVectror2;
	private Transform tmp;
	Vector3 vectorTmp;

	void Awake () {
		vectorTmp = new Vector3 (transform.position.x - Manager.manager.x, transform.position.y - Manager.manager.y, transform.position.z);
		tmp = transform.FindChild ("BeamScale");
		thisVectror2 = new Vector2 (transform.position.x, transform.position.y);
		rad = Mathf.Atan2 (transform.position.y - origin.y, transform.position.x - origin.x);
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Atan2 (transform.position.y - origin.y, transform.position.x - origin.x) * Mathf.Rad2Deg));
		RaycastHit2D alertPos = Physics2D.Raycast (thisVectror2, new Vector2 (-1 * Mathf.Cos(rad), -1 * Mathf.Sin(rad)).normalized, 2000f, 1 << LayerMask.NameToLayer("Alert"));
		Instantiate (alert, alertPos.point, Quaternion.Euler (0, 0, 0));
	}

	public void Shoot () {
		RaycastHit2D col = Physics2D.Raycast (thisVectror2, new Vector2 (-1 * Mathf.Cos(rad), -1 * Mathf.Sin(rad)).normalized, 2000f, 1 << LayerMask.NameToLayer("Player"));
		if (col) {
			if (col.transform.tag == "Body") {
				tmp.localScale = new Vector3 (350, tmp.localScale.y, tmp.localScale.z);
				Manager.manager.SetHealth (-1, amount);
				Manager.manager.cameraShake.Shake (1, 0.2f);
			} else {
				tmp.localScale = new Vector3 (280, tmp.localScale.y, tmp.localScale.z);
				Manager.manager.AddScore (score);
			}
		}
	}

	public void Shrink () {
		tmp.localScale = new Vector3 (1, tmp.localScale.y, tmp.localScale.z);
	}
}
