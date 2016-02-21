using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float ROTATION_DELTA;
	public float CurrentTime;
	public GameObject GunPickup;

	void Start () {
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Terrain"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("TurretBullet"), LayerMask.NameToLayer("Terrain"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("TurretBullet"), LayerMask.NameToLayer("TurretBullet"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("TurretBullet"), LayerMask.NameToLayer("Turret"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Turret"), LayerMask.NameToLayer("Terrain"));

		GameObject.Instantiate( GunPickup );

		CurrentTime = Time.timeSinceLevelLoad;
	}

	void Update(){

		if (Time.time >= (CurrentTime + ROTATION_DELTA) ){
			CurrentTime = Time.time;

			rotatePlatforms( "InnerPlatforms", getRandomRotationSpeed() );
			rotatePlatforms( "OuterPlatforms", getRandomRotationSpeed() );
		}
	}

	private void rotatePlatforms( string platformTag, float rotationSpeed ){
		GameObject[] platforms = GameObject.FindGameObjectsWithTag( platformTag );

			for (int i = 0; i < platforms.Length; i++){
				platform p = platforms[i].GetComponent<platform>();
				p.rotationSpeed = rotationSpeed;
			}
	}

	private float getRandomRotationSpeed(){
		float val = UnityEngine.Random.Range( 1, 3);
		if (UnityEngine.Random.value > 0.5f){
			val *= -1;
		}
		return val;
	}
}
