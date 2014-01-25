using UnityEngine;
using System.Collections;

public class PlayerController : FSMSystem {
	
	//Add the states
	public Character_Idle s_idle;
	public Character_Walk s_walk;
	public Character_Jump s_jump;
	public Character_Falling s_fall;
	public Character_Ladder s_ladder;
	public Character_Stunned s_stun;
	public Character_Interact s_interact;

	
	public bool facingRight = true;
	public bool grounded;
	public float horizAxis;
	public float vertAxis;
	public float moveForce;
	public float jumpForce;
	public float maxSpeed;	

	public float playerNumber;
	public Transform playerGrounder;


	public AudioClip[] aClips;

	
	void Awake(){
		AddState( s_idle );
		s_idle.Parent = this;
		AddState (s_walk);
		s_walk.Parent = this;
		AddState (s_jump);
		s_jump.Parent = this;
		AddState( s_fall );
		s_fall.Parent = this;
		AddState (s_ladder);
		s_ladder.Parent = this;
		AddState( s_stun );
		s_stun.Parent = this;
		AddState( s_interact );
		s_interact.Parent = this;

		//goto first default state
		GoToState( s_idle );
	}

	public string HorizInput(){
		return "Horizontal" + playerNumber.ToString();
	}

	public string JumpInput(){
		return "Jump" + playerNumber.ToString();
	}

	public string VertInput(){
		return "Vertical" + playerNumber.ToString();
	}

	/// <summary>
	/// Flip the direction character is facing
	/// </summary>
	public void Flip(){
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}


	public void LineCasting(){
		Debug.DrawLine (transform.position, playerGrounder.position, Color.cyan);
		grounded = Physics2D.Linecast (transform.position, playerGrounder.position, 1 << LayerMask.NameToLayer ("Middleground"));
	}

	public void playFootsteps() {
		if (!audio.isPlaying) {
			audio.clip = aClips [0];
			audio.Play ();
		}

	}

	public void OnTriggerEnter2D(Collider2D other) {
		vertAxis = Input.GetAxis(VertInput());
		//Debug.Log("Vertical input player " + playerNumber.ToString() + ": " + vertAxis);
		if(other.gameObject.tag == "Ladder" && vertAxis < 0.4){		//Note that a negative vertical input means aiming up with the joystick! (it's weird but is like this...)
			Debug.Log ("Climbing ladder!");
			GoToState(s_ladder);
		}
	}

	public void OnTriggerExit2D(Collider2D other){
		//if(other.gameObject.tag == "Ladder"){
		   Debug.Log ("Trigger exited");
			GoToState(s_fall);
		//}
	}
	
	public void ThrowTrap(){
		GameObject newTrap = GameObject.Instantiate( Resources.Load( "Trap" ), transform.position, Quaternion.identity ) as GameObject;
		newTrap.GetComponent<Trap>().owner = this;
		Vector2 force = new Vector2( horizAxis * 500.0f, 150.0f ) + rigidbody2D.velocity;
		newTrap.rigidbody2D.AddForce( force ); 
		newTrap.rigidbody2D.AddTorque( Random.Range( -25.0f, 25.0f ) );
	}
}
