using UnityEngine;
using System.Collections;

public class CircularGravity : MonoBehaviour {
	public float gravityStrength;

	Vector2 CENTER = new Vector2(0, 0);
	Rigidbody2D objRigidBody;

	// Use this for initialization
	void Start () {
		objRigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		Vector2 gravityDirection = gameObject.transform.position.normalized;
		objRigidBody.AddForce( gravityStrength*gravityDirection );
	}

}
