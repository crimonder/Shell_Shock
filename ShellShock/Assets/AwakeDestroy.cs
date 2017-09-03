using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeDestroy : MonoBehaviour {

	public float time;

	void Start () {
		StartCoroutine (DestroyCoroutine());
	}
	
	IEnumerator DestroyCoroutine () {
		yield return new WaitForSeconds (time);
		Destroy (this.gameObject);
	}
}
