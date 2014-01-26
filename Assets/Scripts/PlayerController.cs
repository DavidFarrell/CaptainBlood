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
	public Character_Wheel s_wheel;
	public GameObject dust;
	public GameObject spray;

	public bool isGoodie = true;

	public Animator playerAnimator;
	public AudioEngine speaker;
	
	public bool facingRight = true;
	public Poster facedPoster;			//A reference to the poster which the player is in front of. Null if there is not any poster behind the player
	public bool grounded;
	public bool canLadder;
	public float horizAxis;
	public float vertAxis;
	public float moveForce;
	public float jumpForce;
	public float maxSpeed;	

	public int playerNumber;
	public Transform playerGrounder;

	public GameObject heldWeapon;
	public int weaponRemaining = 0;
	
	void Awake(){

		playerAnimator = GetComponent<Animator> ();

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
		AddState( s_wheel );
		s_wheel.Parent = this;


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
	
	public string InteractInput(){
		return "Interact" + playerNumber.ToString();
	}
	
	public string WeaponInput(){
		return "Weapon" + playerNumber.ToString();
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


	public bool LineCasting(){

	//	Debug.DrawLine (transform.position, playerGrounder.position, Color.cyan);
	//	grounded = Physics2D.Linecast (transform.position, playerGrounder.position, 1 << LayerMask.NameToLayer ("Middleground"));

		//cast 3 rays down the size of half the height of the player, one in centre, one at left edge, one at right edge
		//bool onGround = false;
		float dist = transform.GetComponent<CircleCollider2D> ().radius + 0.1f;
	//	float dist = transform.GetComponent<BoxCollider2D> ().size.y / 2 + 0.1f ;
		float width = transform.GetComponent<CircleCollider2D> ().radius/2 + 0.02f ;

		Vector3 posDF = transform.position + new Vector3(transform.GetComponent<CircleCollider2D> ().center.x, transform.GetComponent<CircleCollider2D> ().center.y, 0);

		//Debug.DrawRay (posDF, -Vector2.up, Color.magenta, dist);

		RaycastHit2D[] hits;

		hits = Physics2D.RaycastAll (posDF, -Vector2.up, dist);
		for (int i = 0; i < hits.Length; i++ ){
		//	Debug.DrawLine (transform.GetComponent<CircleCollider2D> ().center, hits[i].point, Color.magenta, 5.0f);

			//Debug.Log( "HITS : " + hits[i].transform.tag );

			if ( hits[i].transform.tag == "Middleground" ){

				//Debug.DrawLine (transform.GetComponent<CircleCollider2D> ().center, hits[i].point, Color.magenta, 5.0f);
			//	Debug.Log( "HIT THE GROUND" );
				return true;
			}
			if ( hits[i].transform.tag == "Player" ){
		
				if(hits[i] != this){
					Debug.Log( "Landed on player");
					hits[i].transform.GetComponent<PlayerController>().GoToState(s_stun);
				}
			}

		}

		Vector3 leftSide = posDF + (Vector3.left * width);
		hits = Physics2D.RaycastAll (leftSide, -Vector2.up, dist);
		for (int i = 0; i < hits.Length; i++ ){
			
			//Debug.Log( "LHITS : " + hits[i].transform.tag );
			
			if ( hits[i].transform.tag == "Middleground" ){
				
				Debug.DrawLine (leftSide, hits[i].point, Color.magenta, 5.0f);
				//Debug.Log( "HIT THE GROUND" );
				return true;
			}
		}


		Vector3 rightSide = posDF + (Vector3.right * width);
		hits = Physics2D.RaycastAll (rightSide, -Vector2.up, dist);
		for (int i = 0; i < hits.Length; i++ ){
			
			//Debug.Log( "HITS : " + hits[i].transform.tag );
			
			if ( hits[i].transform.tag == "Middleground" ){
				
				Debug.DrawLine (rightSide, hits[i].point, Color.green, 5.0f);
				//Debug.Log( "HIT THE GROUND" );
				return true;
			}
		}






		return false;
	}
	
	public void CheckInteraction(){
		//Debug.Log ("Interactionfrom player :" + playerNumber);
		//fire a wee ray out of the player...
		RaycastHit2D[] hit;
		if ( !facingRight ){
			hit = Physics2D.RaycastAll( transform.position, Vector3.left, 1.0f );
			Debug.DrawRay( transform.position, Vector3.left , Color.green, 1.0f );
		}else{
			hit = Physics2D.RaycastAll( transform.position, Vector3.right, 1.0f );
			Debug.DrawRay( transform.position, Vector3.right , Color.blue, 1.0f );
		}
		
		bool hadHit = false;
		for ( int i = 0; i < hit.Length; i++ ){
			//Debug.Log("hit a thing: " + hit[i].transform.name);
			if ( hit[i].transform.tag == "Interactable" ){
				Debug.Log( "GOT AN INTERACTABLE GAMEOBJECT :" + hit[i].transform.name );
		


				switch ( hit[i].transform.name ){
				
				case "Poster":
					hit[i].transform.GetComponent<Poster>().ChangePoster( playerNumber );
					if (!isGoodie) {
						playAudioBaddiePoster();
					} else {
						playAudioGoodiePoster();
					}
				
					break;


				case "wheel":
					hit[i].transform.GetComponent<Wheel>().Grab( this );
					break;
				};
			} 
			if ( hadHit ){
				break;
			}
			
		}
	}

	/*
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
	*/

	
	public void ThrowTrap(){
		if (heldWeapon != null) {
			GameObject newTrap = GameObject.Instantiate ( heldWeapon, transform.position, Quaternion.identity) as GameObject;
			newTrap.GetComponent<Trap> ().owner = this;
			newTrap.layer = gameObject.layer;
			Vector2 force = new Vector2 (horizAxis * 500.0f, 150.0f) + rigidbody2D.velocity;
			newTrap.rigidbody2D.AddForce (force); 
			newTrap.rigidbody2D.AddTorque (Random.Range (-25.0f, 25.0f));
			speaker.playSound (AudioEngine.SOUND_TRAP_THROW);
			weaponRemaining--;
			if ( weaponRemaining == 0 ){
				heldWeapon = null;
			}
		}
	}
	
	public void playAudioTrapLand(){

		speaker.playSound (AudioEngine.SOUND_TRAP_LAND);
	}
	
	public void playAudioJump(){
		speaker.playSound (AudioEngine.SOUND_JUMP);
	}
	
	public void playAudioBaddiePoster() {
		speaker.playSound (AudioEngine.SOUND_POSTER_BADDIE);
	}
	
	public void playAudioGoodiePoster() {
		speaker.playSound (AudioEngine.SOUND_POSTER_GOODIE);
	}
	/*
	public void playerDies() {
		GameObject.Find ("GameController").GetComponent <GameController>();
	}*/
}
