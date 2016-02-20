using UnityEngine;
using System.Collections;

public class CircularGravity : MonoBehaviour {
	GameObject parent;
	public float gravityStrength;
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
