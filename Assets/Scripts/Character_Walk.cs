using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Walk : FSMState {

	[System.NonSerialized]
	public PlayerController Parent;

	public int mySpeaker;
	private AudioEngine audioEngine;

	// Use this for initialization
	public override void OnEnter () {
		audioEngine = Parent.speaker;
		mySpeaker = audioEngine.playSound (AudioEngine.SOUND_WALKING, true);
	//	Debug.Log( "Entered " + this );
		Parent.playerAnimator.SetTrigger ("walk");
		//turn on dust
		//Parent.dust.SetActive (true);
		Parent.dust.particleSystem.emissionRate = 5;
	}

	public override void OnExit () {
		audioEngine.stopSound (mySpeaker);
		//turn off dust
		//Parent.dust.SetActive (false);
		Parent.dust.particleSystem.emissionRate = 0;
	//	Debug.Log( "Exited " + this );
	}

	// Update is called once per frame
	public override void OnUpdate () {

	//	Parent.LineCasting();

		// audio


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
		// Jump
		if (Input.GetButtonDown (Parent.JumpInput ())) {

			if ( Parent.LineCasting() ){
				Parent.playAudioJump ();
				Parent.GoToState( Parent.s_jump );
			}

		}

		// Idle
		if (Mathf.Abs (Parent.transform.rigidbody2D.velocity.x) < 0.1f) {
			//Debug.Log("slowed down to idle");
			Parent.GoToState (Parent.s_idle);
		}

		Parent.vertAxis = Input.GetAxis(Parent.VertInput());
		if (Parent.canLadder && (Mathf.Abs (Parent.vertAxis) > 0.1f)) 
		{
			Parent.GoToState(Parent.s_ladder);
		}
		//</ STATE TRANSITIONS>

		
		

		// interact
		if (Input.GetButtonDown (Parent.InteractInput())){
			Parent.CheckInteraction();
		}
		
		if ( Input.GetButtonDown (Parent.WeaponInput ()) ){
		//	Debug.Log("it's a weapon!");
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
