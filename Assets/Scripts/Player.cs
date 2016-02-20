using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class Player : MonoBehaviour {
	private const float ROT_DEAD_ZONE = 0.2f;

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
		float speedX = XCI.GetAxis(XboxAxis.LeftStickX, playerNum) * Speed;
		GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, GetComponent<Rigidbody2D>().velocity.y);
	}
}
