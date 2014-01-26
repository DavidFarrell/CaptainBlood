using UnityEngine;
using System.Collections;

[System.Serializable]
public class DFCharacterWalk : FSMState {
	
	[System.NonSerialized]
	public PlayerController Parent;

	// The speed when walking
	public float walkSpeed = 3.0f;

	public float inAirControlAcceleration = 1.0f;

	// The gravity for the character
	
	public float gravity = 60.0f;
	public float maxFallSpeed = 20.0f;

	// How fast does the character change speeds?  Higher is faster.
	public float speedSmoothing = 20.0f;
	
	// This controls how fast the graphics of the character "turn around" when the player turns around using the controls.
	public float rotationSmoothing = 10.0f;
	
	// The current move direction in x-y.  This will always been (1,0,0) or (-1,0,0)
	[System.NonSerialized]
	public Vector3 direction = Vector3.zero;

	// The current vertical speed
	[System.NonSerialized]
	public float verticalSpeed = 0.0f;

	// The current movement speed.  This gets smoothed by speedSmoothing.
	[System.NonSerialized]
	public float speed = 0.0f;

	// Is the user pressing the joystick left or right?
	
	[System.NonSerialized]
	public bool isMoving = false;

	
	// The last collision flags returned from controller.Move
	[System.NonSerialized]
	public CollisionFlags collisionFlags;

	// We will keep track of an approximation of the character's current velocity, so that we return it from GetVelocity () for our camera to use for prediction.
	[System.NonSerialized]
	public Vector3 velocity;

	// This keeps track of our current velocity while we're not grounded?
	[System.NonSerialized]
	public Vector3 inAirVelocity = Vector3.zero;
	
	
	
	// This will keep track of how long we have we been in the air (not grounded)
	
	[System.NonSerialized]
	
	public float hangTime = 0.0f;



	// Use this for initialization
	public override void OnEnter () {
		Debug.Log( "Entered " + this );
		Parent.playerAnimator.SetTrigger ("walk");
	}
	
	public override void OnExit () {
		Debug.Log( "Exited " + this );
	}
	
	// Update is called once per frame
	public override void OnUpdate () {
		
		// audio
		//Parent.playAudioFootsteps ();

		//retrieve axis info
		Parent.horizAxis = Input.GetAxis(Parent.HorizInput());
		
		if( Mathf.Abs( Parent.transform.rigidbody2D.velocity.x) > 0.6 ){
			if (Mathf.Abs( Parent.transform.rigidbody2D.velocity.x) >= Parent.maxSpeed){
				Parent.transform.rigidbody2D.velocity = new Vector2(Mathf.Sign( Parent.transform.rigidbody2D.velocity.x) * Parent.maxSpeed, Parent.transform.rigidbody2D.velocity.y);	// ... set the player's velocity to the maxSpeed in the x axis.
			}
			else{		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
				Parent.transform.rigidbody2D.AddForce(Vector2.right * Parent.horizAxis * Parent.moveForce);				// ... add a force to the player.
			}
		}
		else{
			//Parent.GoToState( Parent.s_idle );
			if( Parent.horizAxis * Parent.transform.rigidbody2D.velocity.x < Parent.maxSpeed)
				Parent.transform.rigidbody2D.AddForce(Vector2.right * Parent.horizAxis * Parent.moveForce);				// ... add a force to the player.
		}
		
		//<STATE TRANSITIONS>
		if (Input.GetButtonDown (Parent.JumpInput ())) {
			Parent.GoToState( Parent.s_jump );
		}
		if (Mathf.Abs (Parent.transform.rigidbody2D.velocity.x) < 0.1f) {
			Parent.GoToState (Parent.s_idle);
		}


		/*		if (Input.GetButtonDown (Parent.JumpInput()) && Parent.grounded == true ){
				Parent.rigidbody2D.AddForce (Vector2.up * Parent.jumpForce);
		}*/
		
		if ( Input.GetButtonDown("Fire2") ){
			Parent.ThrowTrap();	
		}
		
		// If the input is moving the player right and the player is facing left...
		if( Parent.horizAxis > 0 && !Parent.facingRight){
			// ... flip the player.
			Parent.Flip();
		} else if ( Parent.horizAxis < 0 && Parent.facingRight){
			// ... flip the player.
			Parent.Flip();
		} 
		
	}
}
