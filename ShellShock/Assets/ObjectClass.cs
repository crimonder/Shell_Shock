using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClass : MonoBehaviour {

	public enum Type {
		BULLET,
		FEED,
		RIGHTBOOMERANG,
		LEFTBOOMERANG,
		BEAM,
	}

	public Type type;
	public float amount;
	public float speed;
	public int score;
	public GameObject shellParticle;
//	public ParticleSpawn particleSpawn;

	Vector3 origin = new Vector3(-100, 0, 0);

	void Awake () {
//		particleSpawn = GetComponent<ParticleSpawn> ();
		if (type != Type.RIGHTBOOMERANG && type != Type.LEFTBOOMERANG) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Atan2 (transform.position.y - origin.y, transform.position.x - origin.x) * Mathf.Rad2Deg));
		} else if (type == Type.RIGHTBOOMERANG) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Atan2 (transform.position.y - origin.y, transform.position.x - origin.x) * Mathf.Rad2Deg + 60f));
		} else if (type == Type.LEFTBOOMERANG) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Atan2 (transform.position.y - origin.y, transform.position.x - origin.x) * Mathf.Rad2Deg - 60f));
		}
	}

	void Update () {
		transform.Translate (Vector3.left * Time.deltaTime * speed);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag != "Boom") {
			if (col.tag == "Shield") {
				if (type != Type.FEED) {
//					particleSpawn.Spawn ();
					GameObject sp = Instantiate (shellParticle, this.transform.position, this.transform.rotation);
					Destroy (sp, 3f);
					if (type == Type.LEFTBOOMERANG) {
						sp.transform.rotation = Quaternion.Euler (new Vector3 (this.transform.rotation.x, this.transform.rotation.y, 2 * (Spawner.spawner.joyStick.angle) - (this.transform.rotation.x + 180) - 180));
					} else if (type == Type.RIGHTBOOMERANG) {
						sp.transform.rotation = Quaternion.Euler (new Vector3 (this.transform.rotation.x, this.transform.rotation.y, 3 * (this.transform.rotation.x + 180) - 2 * (Spawner.spawner.joyStick.angle) - 180));
					}
					Spawner.spawner.AddScore (score);
					Spawner.spawner.cameraShake.Shake (0.5f, 0.2f);
				}
			} else if (col.tag == "Body") {
				if (type != Type.FEED) {
					Spawner.spawner.SetHealth (-1, amount);
					Spawner.spawner.cameraShake.Shake (1, 0.2f);
				}
			} else if (col.tag == "Beak") {
				if (type == Type.FEED) {
					Spawner.spawner.SetHealth (1, amount);
					Spawner.spawner.AddScore (score);
				} else {
					Spawner.spawner.SetHealth (-1, amount);
					Spawner.spawner.cameraShake.Shake (1, 0.2f);
				}
			}
			Destroy (this.gameObject);
		}
		if ((type == Type.RIGHTBOOMERANG || type == Type.LEFTBOOMERANG) && col.tag == "Boom") {
			GetComponent<LerpRotation> ().speed = 0.01f;
		}
	}
}
