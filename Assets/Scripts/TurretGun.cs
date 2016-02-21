using UnityEngine;
using System.Collections;

public class TurretGun : MonoBehaviour {
	public Animator GunAnimator;

	public void StopShooting() { 
		GunAnimator.SetBool("shoot", false);
	}
}
