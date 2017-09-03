using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

	public static Spawner spawner;
	public CameraShake cameraShake;
	public JoyStick joyStick;

	public float oriHealth;
	public float curHealth;
	public float minusAmount;
	public float minusDelay;
	public float spawnDelay;
	public GameObject bullet, feed, rightBoomerang, leftBoomerang, beam;
	public Vector3 pos;
	public Image healthbar;
	public int[] deg;
	private float rad;
	public float height;
	public float x, y;
	public int score = 0;
	public Text scoreText;
	[SerializeField] private Transform shadow;
	public GameObject menuObject;
	public GameObject startObject;
	public GameObject startObjectCanvas;
	private bool started;

	public void Timescale () {
		if (Time.timeScale == 1f) {
			Time.timeScale = 0.2f;
		} else {
			Time.timeScale = 1f;
		}
	}

	public void Quit () {
		Application.Quit ();
	}

	void Awake () {
		rad = (float)deg [Random.Range (0, deg.Length)] * Mathf.Deg2Rad;
		x = height * Mathf.Cos (rad);
		y = height * Mathf.Sin (rad);
		shadow.localPosition = new Vector3 (-0.125f * x, -0.125f * y, 0);
		Spawner.spawner = this;
		curHealth = oriHealth;
	}

	public void GameStart () {
		if (!started) {
			menuObject.SetActive (false);
			startObject.SetActive (true);
			startObjectCanvas.SetActive (true);
			curHealth = oriHealth;
			score = 0;
			scoreText.text = score.ToString ();
			started = true;
			StartCoroutine (HealthCoroutine ());
			StartCoroutine (SpawnCoroutine ());
		}
	}

	public void AddScore (int s) {
		score += s;
		scoreText.text = score.ToString ();
	}

	IEnumerator HealthCoroutine () {
		while (curHealth > 0) {
			yield return new WaitForSeconds (minusDelay);
			curHealth -= minusAmount;
			SetHealth (0, 0);
		}
		started = false;
		Debug.LogError("GameOver");
	}

	public void SetHealth (int pm, float amount) {
		switch (pm) {
		case -1:
			curHealth -= amount;
			if (curHealth < 0f) {
				curHealth = 0f;
			}
			break;
		case 1:
			curHealth += amount;
			if (curHealth > 100f) {
				curHealth = 100f;
			}
			break;
		}
		float _value = 1f;
		if ((float)curHealth /oriHealth <= 1) {
			_value = (float)curHealth /oriHealth;
		}else if (curHealth < 0) {
			curHealth = 0;
			_value = 0;
		}else {
			_value = 1f;
		}

		healthbar.rectTransform.localScale = new Vector3 (_value, 1, 1);
	}

	int dice;
	int angle;
	IEnumerator SpawnCoroutine () {
		while (curHealth > 0) {
			yield return new WaitForSeconds (spawnDelay);
			dice = Random.Range (0, 6);
			angle = Random.Range (0, 360);
			pos = new Vector3 (65f * Mathf.Cos (angle * Mathf.Deg2Rad) - 100, 65f * Mathf.Sin (angle * Mathf.Deg2Rad), 0);
			switch (dice) {
			case 1:
				Instantiate (beam, pos, Quaternion.Euler (0, 0, 0));
				break;
			case 2:
				Instantiate (bullet, pos, Quaternion.Euler (0, 0, 0));
				break;
			case 3:
				Instantiate (feed, pos, Quaternion.Euler (0, 0, 0));
				break;
			case 4:
				
				int r = Random.Range (0, 2);
				if (r == 0) {
					Instantiate (rightBoomerang, pos, Quaternion.Euler (0, 0, 0));
				} else {
					Instantiate (leftBoomerang, pos, Quaternion.Euler (0, 0, 0));
				}
				break;
			case 5:
			case 6:
				Instantiate (bullet, pos, Quaternion.Euler (0, 0, 0));
				break;
			}
		}
	}
}
