using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Idle : FSMState {
	
	[System.NonSerialized]
	public PlayerController Parent;

	// Use this for initialization
	public override void OnEnter () {
		//Debug.Log( "Entered " + this );
		Parent.playerAnimator.SetTrigger ("idle");
	}
	
	// Update is called once per frame
	public override void OnUpdate () {
	
		// <STATE TRANSITIONS>

		// jump
		if (Input.GetButtonDown (Parent.JumpInput ())) {
			Parent.GoToState( Parent.s_jump );
		}
		// </STATE TRANSITIONS>


		// <INTERACTIONS>
		// posters
		if (Input.GetButtonDown (Parent.InteractInput())){
			Debug.Log ("Interact button pressed: X");
			if(Parent.facedPoster){
				if(Parent.playerNumber == 1){
					Parent.facedPoster.ChangePoster(2);
				}else if(Parent.playerNumber == 2){
					Parent.facedPoster.ChangePoster(1);
				}
			}else{
				Debug.Log ("The poster was null");
			}
		}
		//</INTERACTIONS>

		//retrieve axis info
		Parent.horizAxis = Input.GetAxis(Parent.HorizInput());
		
		if ( Parent.horizAxis > 0.1f || Parent.horizAxis < -0.1f ){
			Parent.GoToState( Parent.s_walk );	
		}


		if ( Input.GetButtonDown (Parent.WeaponInput ()) ){
			Parent.ThrowTrap();	
		}

		// If the input is moving the player right and the player is facing left...
		if( Parent.horizAxis > 0 && !Parent.facingRight){
			// ... flip the player.
			Parent.Flip();
		}
		
	}


}
