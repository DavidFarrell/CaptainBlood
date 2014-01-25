using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Walk : FSMState {

	[System.NonSerialized]
	public PlayerController Parent;


	private AudioSource baddieFootsteps;


	// Use this for initialization
	public override void OnEnter () {
		Debug.Log( "Entered " + this );
	}
	

	// Update is called once per frame
	public override void OnUpdate () {

		Parent.LineCasting();

		// audio
		Parent.playFootsteps ();


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
		
		
		if (Input.GetButtonDown (Parent.JumpInput()) && Parent.grounded == true ){
				Parent.rigidbody2D.AddForce (Vector2.up * Parent.jumpForce);
		}
		
		// If the input is moving the player right and the player is facing left...
		if( Parent.horizAxis > 0 && !Parent.facingRight){
			// ... flip the player.
			Parent.Flip();
		}
	
	}
}
