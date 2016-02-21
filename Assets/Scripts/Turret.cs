using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class Turret : MonoBehaviour {
	private const float ROT_DEAD_ZONE = 0.2f;

	public GameObject TurretBase;
	public GameObject TurretGun;
	public GameObject Bullet;
	public Transform BulletSpawn;
	public Animator GunAnimator;
	public Animator BaseAnimator;
    public AudioSource shootSound;
    public AudioSource killSound;
	public float Speed;
	public float BulletSpeed;
	public float FireRate;
	public Color color;

	private int playerNum = 2;
	private Quaternion rotation;
	private float currAngle;
	private float distToCenter;
	private float lastFireTime;
	private bool movingRight = true;
	private PlayerManager playerManager;

	private int health = 1;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

	GameObject myHealth1, myHealth2, myHealth3;

	public void SetPlayerNum(int playerNum) {
		this.playerNum = playerNum;
        string h1string = "health1_" + playerNum;
        string h2string = "health2_" + playerNum;
        string h3string = "health3_" + playerNum;

        myHealth1 = GameObject.FindGameObjectWithTag(h1string);
        myHealth2 = GameObject.FindGameObjectWithTag(h2string);
        myHealth3 = GameObject.FindGameObjectWithTag(h3string);

        Destroy( myHealth3 );
        Destroy( myHealth2 );
      //  Invoke("Hit", 1);
    }
		
	public void SetPlayerManager(PlayerManager manager) {
		playerManager = manager;
	}

	public void Hit() {
		
        killSound.Play();
        Invoke("DestroyMe", 2.0f);


    }

    void DestroyMe()
{
        playerManager.PlayerDied(playerNum);
        Destroy(myHealth1);
        Destroy(this.gameObject);

}

public void SetColor(Color c){
		color = c;
	}

	void Start() {
		distToCenter = Mathf.Sqrt(transform.position.x * transform.position.x + transform.position.y * transform.position.y);
		currAngle = Mathf.Atan2(TurretBase.transform.position.y, TurretBase.transform.position.x);
		lastFireTime = Time.timeSinceLevelLoad;
	}

	void Update() {
		setTurretRotation();
		setMovement();
		setBaseRotation();
		checkFire();
	}

	void FixedUpdate() {
		setMovement();
		setBaseRotation();
	}

	private void setTurretRotation() {
		float rotX = XCI.GetAxis(XboxAxis.RightStickX, playerNum);
		float rotY = -XCI.GetAxis(XboxAxis.RightStickY, playerNum);
		float angle = Mathf.Atan2(-rotY, rotX) * Mathf.Rad2Deg + 90f;
		Quaternion prevRot = transform.rotation;

		if (Mathf.Abs(rotX) > ROT_DEAD_ZONE || Mathf.Abs(rotY) > ROT_DEAD_ZONE) {
			TurretGun.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}

	private void setBaseRotation() {
		Vector2 centerLocation = gameObject.transform.position.normalized;
		float angleForFeet = Mathf.Atan2(centerLocation.y, centerLocation.x) * 180f / Mathf.PI - 90;
		TurretBase.transform.localEulerAngles = new Vector3(0, 0, angleForFeet);
	}

	private void setMovement() {
		bool increaseAngle = XCI.GetAxis(XboxAxis.LeftStickX, playerNum) > 0;
		bool decreaseAngle = XCI.GetAxis(XboxAxis.LeftStickX, playerNum) < 0;
		bool moving = BaseAnimator.GetBool("moving");

		if (increaseAngle) {
			if (!moving) {
				BaseAnimator.SetBool("moving", true);
			}
			if (!movingRight) {
				Vector3 scale = TurretBase.transform.localScale;
				TurretBase.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
				movingRight = true;
			}

			float nextAngle = currAngle + Speed;	
			float nextX = Mathf.Cos(nextAngle) * distToCenter;
			float nextY = Mathf.Sin(nextAngle) * distToCenter;
			currAngle = nextAngle;

			transform.position = new Vector2(nextX, nextY);
		}
		else if (decreaseAngle) {
			if (!moving) {
				BaseAnimator.SetBool("moving", true);
			}
			if (movingRight) {
				Vector3 scale = TurretBase.transform.localScale;
				TurretBase.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
				movingRight = false;
			}

			float nextAngle = currAngle - Speed;
			float nextX = Mathf.Cos(nextAngle) * distToCenter;
			float nextY = Mathf.Sin(nextAngle) * distToCenter;
			currAngle = nextAngle;

			transform.position = new Vector2(nextX, nextY);
		}
		else {
			if (moving) {
				BaseAnimator.SetBool("moving", false);	
			}
		}
	}

	private void checkFire() {
		if (XCI.GetAxis(XboxAxis.RightTrigger, playerNum) > 0.1f && Time.timeSinceLevelLoad - lastFireTime > FireRate) {
			GameObject bullet = Instantiate(Bullet, BulletSpawn.position,  TurretGun.transform.rotation) as GameObject;
			GunAnimator.SetBool("shoot", true);
			Invoke("stopShooting", 0.15f);
            shootSound.Play();
			bullet.GetComponent<Rigidbody2D>().velocity = -TurretGun.transform.up * (BulletSpeed);
			lastFireTime = Time.timeSinceLevelLoad;
		}
	}

	private void stopShooting() {
		GunAnimator.SetBool("shoot", false);
	}
}
