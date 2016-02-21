using UnityEngine;
using System.Collections;

public class GunPickup : MonoBehaviour {

	public float maxScale;
	public float minScale;
	public float Step;

	private Vector3 minVec;
	private Vector3 maxVec;
	private Vector3 velocity;

	private int direction;

	// Use this for initialization
	void Start () {
		direction = 1;
		minVec = new Vector3(minScale, minScale, 1);
		maxVec = new Vector3(maxScale, maxScale, 1);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			Player p = col.gameObject.GetComponent<Player>();
			p.PickedUpGun();
		}
		Destroy( this.gameObject );
	}
	
	// Update is called once per frame
	void Update () {
		if (direction > 0){
			transform.localScale = smoothVec( transform.localScale, maxVec );
		}
		else{
			transform.localScale = smoothVec( transform.localScale, minVec );	
		}

		if (transform.localScale.Equals(maxVec) || transform.localScale.Equals(minVec)){
			direction *= -1;
		}
	}

	private Vector3 smoothVec( Vector3 current, Vector3 towards ){
		return Vector3.Lerp(current, towards, Step );
	}
}
