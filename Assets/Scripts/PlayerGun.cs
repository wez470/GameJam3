using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class PlayerGun : MonoBehaviour {
	private const float ROT_DEAD_ZONE = 0.2f;

	private Player player;

	void Start () {
		player = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		setRotation();
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
}
