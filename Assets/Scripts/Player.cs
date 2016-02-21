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

	private string[] terrain = { "Terrain" };
	private int playerNum = 1;
	private Quaternion rotation;
	private PlayerManager playerManager;

    private int health = 3;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    CircleCollider2D myCircleCollider;
    GameObject myHealth1, myHealth2, myHealth3;

    void Start()
    {
        myCircleCollider = GetComponent<CircleCollider2D>();

        scaleX = this.GetComponent<Transform>().localScale.x;
        scaleY = this.GetComponent<Transform>().localScale.y;
        scaleZ = this.GetComponent<Transform>().localScale.z;
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
        }
        else if (health == 1)
        {
            Destroy(myHealth2.gameObject);
        }
        else if (health < 1)
        {
			playerManager.PlayerDied(playerNum);
            Destroy(myHealth1.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void PickedUpGun(){

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
			RaycastHit2D cast = Physics2D.Raycast( gameObject.transform.position, down, 0.5f, LayerMask.GetMask( "Terrain" ) );
			if ( cast.collider != null ){
				gameObject.GetComponent<Rigidbody2D>().AddForce( -1*down*jumpStrength );
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
