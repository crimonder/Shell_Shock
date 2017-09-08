﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public Camera cam;

	public float shakeAmount = 0;

	void Start () {
		cam = GetComponent<Camera> ();
		Manager.manager.cameraShake = this;
	}

	public void Shake (float amt, float length) {
		shakeAmount = amt;
		InvokeRepeating ("DoShake", 0, 0.05f);
		Invoke ("StopShake", length);
	}

	void DoShake () {
		if (shakeAmount > 0) {
			Vector3 camPos = cam.transform.position;

			float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
			float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
			camPos.x += offsetX;
			camPos.y += offsetY;

			cam.transform.position = camPos;
		}
	}

	void StopShake () {
		CancelInvoke ("DoShake");
		cam.transform.localPosition = Vector3.zero;
	}
}
