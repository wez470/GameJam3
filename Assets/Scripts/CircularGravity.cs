using UnityEngine;
using System.Collections;

public class CircularGravity : MonoBehaviour {
	GameObject parent;
	public float gravityStrength;
	/*Player starts with 0 degrees rotation meaning his feet point down.
	  We need to add that to the calculation for the direction to point his feet.
	*/
	private const float PLAYER_ANGLE_CORRECTION = 90f;
	Rigidbody2D objRigidBody;

	// Use this for initialization
	void Start () {
		objRigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		Vector2 gravityDirection = gameObject.transform.position.normalized;
		objRigidBody.AddForce( gravityStrength*gravityDirection );
		rotateBody( gravityDirection );
	}

	private void rotateBody(Vector2 gravityDirection){
		float angleForFeet = Mathf.Atan2( gravityDirection.y, gravityDirection.x )*180f/Mathf.PI + PLAYER_ANGLE_CORRECTION;
		gameObject.transform.eulerAngles = new Vector3( 0, 0, angleForFeet );
	}
}
