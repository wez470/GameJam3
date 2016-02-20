using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	void Start () {
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Terrain"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Bullet"));
	}
}
