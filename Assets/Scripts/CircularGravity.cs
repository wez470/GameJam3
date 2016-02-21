using UnityEngine;
using System.Collections;

public class CircularGravity : MonoBehaviour {
	GameObject parent;
	public float gravityStrength;
	Rigidbody2D body;

	string[] terrain = {"Terrain"};

	// Use this for initialization
	void Start () {
		body = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		Vector2 gravityDirection = gameObject.transform.position.normalized;


		int terrainMask = LayerMask.NameToLayer( "Terrain" );
		RaycastHit2D cast = Physics2D.Raycast( gameObject.transform.position, gravityDirection, float.MaxValue, LayerMask.GetMask( terrain ) );
		body.AddForce( -1*gravityStrength*cast.normal );

	}
}
