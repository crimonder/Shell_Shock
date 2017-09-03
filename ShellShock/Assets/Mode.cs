using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mode : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

	public JoyStick joyStick;
	public Transform beak;
	public GameObject shield;
	public bool eating;

	public void OnPointerDown (PointerEventData ped) {
		eating = true;
		GetComponent<Image> ().color = new Color (200f / 255f, 200f / 255f, 200f / 255f, 1);
		joyStick.shield.rotation = Quaternion.Euler (0, 0, 0);
		beak.localPosition = new Vector3 (0.4f * Mathf.Cos (joyStick.angle), 0.3f * Mathf.Sin (joyStick.angle), 0);
		shield.SetActive (false);
		beak.GetComponent<CapsuleCollider2D> ().enabled = true;
	}

	public void OnPointerUp (PointerEventData ped) {
		eating = false;
		beak.localPosition = new Vector3 (0, -0.2f, 0);
		joyStick.shield.rotation = Quaternion.Euler (0, 0, joyStick.angle * Mathf.Rad2Deg);
		GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		shield.SetActive (true);
		beak.GetComponent<CapsuleCollider2D> ().enabled = false;
	}

	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			eating = true;
			GetComponent<Image> ().color = new Color (200f / 255f, 200f / 255f, 200f / 255f, 1);
			joyStick.shield.rotation = Quaternion.Euler (0, 0, 0);
			beak.localPosition = new Vector3 (0.4f * Mathf.Cos (joyStick.angle), 0.3f * Mathf.Sin (joyStick.angle), 0);
			shield.SetActive (false);
			beak.GetComponent<CapsuleCollider2D> ().enabled = true;
		} else if (Input.GetButtonUp ("Jump")) {
			eating = false;
			beak.localPosition = new Vector3 (0, -0.2f, 0);
			joyStick.shield.rotation = Quaternion.Euler (0, 0, joyStick.angle * Mathf.Rad2Deg);
			GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			shield.SetActive (true);
			beak.GetComponent<CapsuleCollider2D> ().enabled = false;
		}
	}
}
