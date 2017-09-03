using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour {

	public GameObject shellParticle;

	public void Spawn () {
		GameObject sp = Instantiate (shellParticle, this.transform.position, this.transform.rotation);
		Destroy (sp, 3f);
	}
}
