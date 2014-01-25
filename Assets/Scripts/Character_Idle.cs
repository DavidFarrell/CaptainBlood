using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Idle : FSMState {
	
	[System.NonSerialized]
	public PlayerController Parent;


	// Use this for initialization
	public override void OnEnter () {
		Debug.Log( "Entered " + this );
	}
	
	// Update is called once per frame
	public override void OnUpdate () {
	
	//	Debug.Log( "UPDATING IDLE!!" );
		
		//retrieve axis info
		Parent.horizAxis = Input.GetAxis(Parent.HorizInput());
		
		if ( Parent.horizAxis > 0.1f || Parent.horizAxis < -0.1f ){
			Parent.GoToState( Parent.s_walk );	
		}
		
	//	if( Mathf.Abs( Parent.transform.rigidbody2D.velocity.x) > 0.6 ){
			
	//		Parent.GoToState( Parent.s_walk );
	//		if (Mathf.Abs( Parent.transform.rigidbody2D.velocity.x) >= Parent.maxSpeed){
	//			Parent.transform.rigidbody2D.velocity = new Vector2(Mathf.Sign( Parent.transform.rigidbody2D.velocity.x) * Parent.maxSpeed, Parent.transform.rigidbody2D.velocity.y);	// ... set the player's velocity to the maxSpeed in the x axis.
	//		}
	//		else{						// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
	//			Parent.transform.rigidbody2D.AddForce(Vector2.right * Parent.horizAxis * Parent.moveForce);				// ... add a force to the player.
	//		}
	//	}
	//	else{
	//		if( Parent.horizAxis * Parent.transform.rigidbody2D.velocity.x < Parent.maxSpeed)
	//			Parent.transform.rigidbody2D.AddForce(Vector2.right * Parent.horizAxis * Parent.moveForce);				// ... add a force to the player.
	//	}
		
//		if (Input.GetButtonDown ("Jump1") && grounded == true ){
//				Parent.rigidbody2D.AddForce (Vector2.up * Parent.jumpForce);
//		}
		
		// If the input is moving the player right and the player is facing left...
		if( Parent.horizAxis > 0 && !Parent.facingRight){
			// ... flip the player.
			Parent.Flip();
		}
		
	}
}
