using UnityEngine;
using System.Collections;

public class TurretBase : MonoBehaviour {
	public Animator BaseAnimator;

	public void StopMoving() { 
		BaseAnimator.SetBool("moving", false);	
	}
}
