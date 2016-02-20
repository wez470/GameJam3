using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class Turret : MonoBehaviour {
	private const float ROT_DEAD_ZONE = 0.2f;

	public GameObject TurretBase;
	public GameObject TurretGun;
	public GameObject Bullet;
	public Transform BulletSpawn;
	public float Speed;

	private int playerNum = 2;
	private Quaternion rotation;

	public void SetPlayerNum(int playerNum) {
		this.playerNum = playerNum;
	}

	void Update() {
		setRotation();
		setMovement();
		checkFire();
	}

	private void setRotation() {
		float rotX = XCI.GetAxis(XboxAxis.RightStickX, playerNum);
		float rotY = -XCI.GetAxis(XboxAxis.RightStickY, playerNum);
		float angle = Mathf.Atan2(-rotY, rotX) * Mathf.Rad2Deg + 90f;
		Quaternion prevRot = transform.rotation;

		if (Mathf.Abs(rotX) > ROT_DEAD_ZONE || Mathf.Abs(rotY) > ROT_DEAD_ZONE) {
			TurretGun.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}

	private void setMovement() {
		float speedX = XCI.GetAxis(XboxAxis.LeftStickX, playerNum) * Speed;
		GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, GetComponent<Rigidbody2D>().velocity.y);
	}

	private void checkFire() {
		if(XCI.GetAxis(XboxAxis.RightTrigger, playerNum) > 0.1f) {
			GameObject bullet = Instantiate(Bullet, BulletSpawn.position,  TurretGun.transform.rotation) as GameObject;
			bullet.GetComponent<Rigidbody2D>().velocity = -TurretGun.transform.up * (Speed + 1);
		}
	}
}
