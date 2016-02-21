using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class Player : MonoBehaviour {
    float scaleX, scaleY, scaleZ;

	/*Player starts with 0 degrees rotation meaning his feet point down.
	  We need to add that to the calculation for the direction to point his feet.
	*/
	private const float PLAYER_ANGLE_CORRECTION = 90f;

	public float Speed;
	public float jumpStrength;

	PlayerGun gun;

	bool hasGun;

	private string[] terrain = { "Terrain" };
	private int playerNum = 1;
	private Quaternion rotation;
	private PlayerManager playerManager;
    public AudioSource dieSound;
    public AudioSource hitSound;
    public AudioSource jumpSound;

	private Color color;

    private int health = 3;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    CircleCollider2D myCircleCollider;
    GameObject myHealth1, myHealth2, myHealth3;

    void Start()
    {
        myCircleCollider = GetComponent<CircleCollider2D>();
        hasGun = false;
        scaleX = this.GetComponent<Transform>().localScale.x;
        scaleY = this.GetComponent<Transform>().localScale.y;
        scaleZ = this.GetComponent<Transform>().localScale.z;

        gun = gameObject.GetComponentInChildren<PlayerGun>();
        gun.gameObject.SetActive( false );

        InvokeRepeating("Hit", 4, 1);
    }

	public void SetPlayerManager(PlayerManager manager) {
		playerManager = manager;
	}

    public void SetPlayerNum(int playerNum) {
		this.playerNum = playerNum;
        string h1string = "health1_" + playerNum;
        string h2string = "health2_" + playerNum;
        string h3string = "health3_" + playerNum;

        myHealth1 = GameObject.FindGameObjectWithTag(h1string);
        myHealth2 = GameObject.FindGameObjectWithTag(h2string);
        myHealth3 = GameObject.FindGameObjectWithTag(h3string);
    }

	public int GetPlayerNum() {
		return playerNum;
	}

    public void Hit()
    {
        health--;
       // print(health);
        if (health == 2)
        {
            Destroy(myHealth3.gameObject);
            hitSound.Play();
        }
        else if (health == 1)
        {
            Destroy(myHealth2.gameObject);
            hitSound.Play();
        }
        else if (health < 1)
        {
            dieSound.Play();
            playerManager.PlayerDied(playerNum);
            Destroy(myHealth1.gameObject);
            Invoke("DestroyMe", 0.5f);  
        }
    }

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    public void PickedUpGun(){
    	enableGun();
	}

	private void enableGun(){
		hasGun = true;

		gun.gameObject.SetActive( true );
		gun.SetAmmo( 3 );
	}

	public void SetColor( Color c ){
		color = c;

		foreach( SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>() ){
			if (sr.transform.gameObject.tag == "playerPants" ){
				sr.color = c;
			}
		}
	}
		
	void FixedUpdate() {
        //don't let him grow!
        transform.GetComponent<Transform>().localScale = new Vector3(scaleX, scaleY, scaleZ);

        setMovement();
        if (gameObject.GetComponent<Rigidbody2D>() == null){
            this.gameObject.AddComponent<Rigidbody2D>();
        }
    
	}

	private void setMovement() {
		Vector2 down = gameObject.transform.position.normalized;
		Vector2 left = new Vector2( -down.y, down.x );
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		Vector2 currentVelocity = body.velocity;

		maybeJump( down );
		rotateBody( down );

		Vector2 velocity = ( XCI.GetAxis(XboxAxis.LeftStickX, playerNum) * Speed * left);
		body.AddForce( velocity );
	}

	private void maybeJump(Vector2 down){
		if (XCI.GetButtonDown(XboxButton.A, playerNum) ){
			RaycastHit2D cast = Physics2D.Raycast( gameObject.transform.position, down, 0.75f, LayerMask.GetMask( "Terrain" ) );
			if ( cast.collider != null ){
				gameObject.GetComponent<Rigidbody2D>().AddForce( -1*down*jumpStrength );
                jumpSound.Play();
			}
		}
	}

	private void rotateBody(Vector2 gravityDirection){
		string[] terrain = {"Terrain"};
		RaycastHit2D cast = Physics2D.Raycast( gameObject.transform.position, gravityDirection, float.MaxValue, LayerMask.GetMask( terrain ) );
		float angleForFeet = Mathf.Atan2(-1f*cast.normal.y, -1f*cast.normal.x )*180f/Mathf.PI + PLAYER_ANGLE_CORRECTION;

		float smoothedAngle = Mathf.LerpAngle( gameObject.transform.eulerAngles.z, angleForFeet, 0.25f );
		gameObject.transform.eulerAngles = new Vector3( 0, 0, smoothedAngle );
	}

}
