using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
	void Update() {
		// Clean up bullets that managed to spawn off the screen
		if (Mathf.Abs(transform.position.y) > 20) {
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Turret") {
			coll.gameObject.GetComponent<Turret>().Hit();
			Destroy(this.gameObject);
		}
	}

	void OnBecameInvisible() {
		Destroy(this.gameObject);	
	}
}
