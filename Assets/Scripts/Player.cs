using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class Player : MonoBehaviour {
	private const float ROT_DEAD_ZONE = 0.2f;

	/*Player starts with 0 degrees rotation meaning his feet point down.
	  We need to add that to the calculation for the direction to point his feet.
	*/
	private const float PLAYER_ANGLE_CORRECTION = 90f;

	public float Speed;

	private int playerNum = 1;
	private Quaternion rotation;

	public void SetPlayerNum(int playerNum) {
		this.playerNum = playerNum;
	}
		
	void Update() {
		setMovement();
	}

	private void setMovement() {
		Vector2 down = gameObject.transform.position.normalized;
		Vector2 left = new Vector2( -down.y, down.x );
		Vector2 currentVelocity = GetComponent<Rigidbody2D>().velocity;

		rotateBody( down );

		Vector2 velocity = ( XCI.GetAxis(XboxAxis.LeftStickX, playerNum) * Speed * left);

		GetComponent<Rigidbody2D>().velocity = currentVelocity + velocity;
	}

	private void rotateBody(Vector2 gravityDirection){
		float angleForFeet = Mathf.Atan2( gravityDirection.y, gravityDirection.x )*180f/Mathf.PI + PLAYER_ANGLE_CORRECTION;
		gameObject.transform.eulerAngles = new Vector3( 0, 0, angleForFeet );
	}
}
