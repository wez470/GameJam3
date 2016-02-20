using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Destroy(coll.gameObject);
			Destroy(this.gameObject);
		}
	}

	void OnBecameInvisible() {
		Destroy(this.gameObject);	
	}
}
