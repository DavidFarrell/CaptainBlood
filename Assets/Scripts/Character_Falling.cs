﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Falling : FSMState {
	
	[System.NonSerialized]
	public PlayerController Parent;
	
	// Use this for initialization
	public override void OnEnter () {
		Debug.Log( "Entered " + this );
		
		Parent.playerAnimator.SetTrigger ("fall");
	}
	
	// Use this for initialization
	public override void OnExit () {
		Debug.Log( "Exiting " + this );
	}
	
	public override void OnUpdate(){
		//	Debug.Log( "UPDATING FALL!!" );
		
		//if (Parent.rigidbody2D.velocity.y <= 0) {
			// Are we still in the air? If not go idle
			if (Parent.LineCasting ())
			{
				Debug.Log("landing?");
				Parent.speaker.playSound(AudioEngine.SOUND_POSTER_LAND_NOISE);
				Parent.GoToState (Parent.s_idle);
			}
		//}

		// interact
		if (Input.GetButtonDown (Parent.InteractInput())){
			Parent.CheckInteraction();
		}

		if ( Input.GetButtonDown (Parent.WeaponInput ()) ){
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

		//retrieve axis info
		Parent.horizAxis = Input.GetAxis(Parent.HorizInput());
	//	if (Parent.grounded) {
	//		Parent.GoToState (Parent.s_idle);
	//	}
		Parent.vertAxis = Input.GetAxis(Parent.VertInput());
		if (Parent.canLadder && (Mathf.Abs (Parent.vertAxis) > 0.1f)) 
		{
			Parent.GoToState(Parent.s_ladder);
		}
	}
	
	
}
