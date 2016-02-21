using UnityEngine;
using System.Collections;

public class TurretGun : MonoBehaviour {
	public Animator GunAnimator;

	public void Test() {
		Debug.Log("TEST");
		GetComponent<Animator>().SetBool("shoot", false);
	}

	public void StopShooting() { 
		Debug.Log("SET FALSE");
		GunAnimator.SetBool("shoot", false);
	}
}
