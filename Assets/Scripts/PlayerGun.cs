using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class PlayerGun : MonoBehaviour {
	private const float ROT_DEAD_ZONE = 0.2f;

	public GameObject Bullet;
	public Transform BulletSpawn;
	public float BulletSpeed;
	public float FireRate;

	private Player player;
	private float lastFireTime;

	void Start () {
		player = GetComponentInParent<Player>();
		lastFireTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		setRotation();
		checkFire();
	}

	private void setRotation() {
		float rotX = XCI.GetAxis(XboxAxis.RightStickX, player.GetPlayerNum());
		float rotY = -XCI.GetAxis(XboxAxis.RightStickY, player.GetPlayerNum());
		float angle = Mathf.Atan2(-rotY, rotX) * Mathf.Rad2Deg;
		Quaternion prevRot = transform.rotation;

		if (Mathf.Abs(rotX) > ROT_DEAD_ZONE || Mathf.Abs(rotY) > ROT_DEAD_ZONE) {
			gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}

	private void checkFire() {
		if (XCI.GetAxis(XboxAxis.RightTrigger, player.GetPlayerNum()) > 0.1f && Time.timeSinceLevelLoad - lastFireTime > FireRate) {
			GameObject bullet = Instantiate(Bullet, BulletSpawn.position,  transform.rotation) as GameObject;
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f * transform.up.y, transform.up.x) * (-1 * BulletSpeed);
			lastFireTime = Time.timeSinceLevelLoad;
		}
	}
}
