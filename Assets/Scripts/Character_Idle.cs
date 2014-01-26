using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Idle : FSMState {
	
	[System.NonSerialized]
	public PlayerController Parent;

	// Use this for initialization
	public override void OnEnter () {
		Debug.Log( "Entered " + this );
		Parent.playerAnimator.SetTrigger ("idle");
	}
	
	// Update is called once per frame
	public override void OnUpdate () {
	
		// <STATE TRANSITIONS>

		// jump
		if (Input.GetButtonDown (Parent.JumpInput())) {
			if ( Parent.LineCasting() ){
				Parent.GoToState( Parent.s_jump );
			}
		}
		// </STATE TRANSITIONS>


		// <INTERACTIONS>
		// interact
		if (Input.GetButtonDown (Parent.InteractInput())){
			Parent.CheckInteraction();
		}
		//</INTERACTIONS>


		//retrieve axis info
		Parent.horizAxis = Input.GetAxis(Parent.HorizInput());
		
		if ( Parent.horizAxis > 0.1f || Parent.horizAxis < -0.1f ){
			Parent.GoToState( Parent.s_walk );	
		}
		Parent.vertAxis = Input.GetAxis(Parent.VertInput());
		if (Parent.canLadder && (Mathf.Abs (Parent.vertAxis) > 0.1f)) 
		{
			Parent.GoToState(Parent.s_ladder);
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
