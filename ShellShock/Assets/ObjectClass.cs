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
					GameObject sp = Instantiate (shellParticle, this.transform.position, this.transform.rotation);
					Destroy (sp, 3f);
//					if (type == Type.RIGHTBOOMERANG) {
////						Debug.LogError (this.transform.rotation.z * Mathf.Rad2Deg + " " + Spawner.spawner.joyStick.angle * Mathf.Rad2Deg + " " + (2 * (Spawner.spawner.joyStick.angle) - (this.transform.rotation.z + 180) - 180).ToString ());
//						sp.transform.rotation = Quaternion.Euler (this.transform.rotation.x, this.transform.rotation.y, 2 * (Spawner.spawner.joyStick.angle) - (this.transform.rotation.z + 180) - 210);
//					} else if (type == Type.LEFTBOOMERANG) {
////						Debug.LogError (this.transform.rotation.z * Mathf.Rad2Deg + " " + Spawner.spawner.joyStick.angle * Mathf.Rad2Deg + " " + (3 * (this.transform.rotation.z + 180) - 2 * (Spawner.spawner.joyStick.angle) - 180).ToString ());
//						sp.transform.rotation = Quaternion.Euler (this.transform.rotation.x, this.transform.rotation.y, 3 * (this.transform.rotation.z + 180) - 2 * (Spawner.spawner.joyStick.angle) - 150);
//					}
					Manager.manager.AddScore (score);
					Manager.manager.cameraShake.Shake (0.1f, 0.2f);
				}
			} else if (col.tag == "Body") {
				if (type != Type.FEED) {
					Manager.manager.SetHealth (-1, amount);
					Manager.manager.cameraShake.Shake (1, 0.2f);
				}
			} else if (col.tag == "Beak") {
				if (type == Type.FEED) {
					Manager.manager.SetHealth (1, amount);
					Manager.manager.AddScore (score);
				} else {
					Manager.manager.SetHealth (-1, amount);
					Manager.manager.cameraShake.Shake (1, 0.2f);
				}
			}
			Destroy (this.gameObject);
		}
		if ((type == Type.RIGHTBOOMERANG || type == Type.LEFTBOOMERANG) && col.tag == "Boom") {
			GetComponent<LerpRotation> ().speed = 0.01f;
		}
	}
}
