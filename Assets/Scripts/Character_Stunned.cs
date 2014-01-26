using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Stunned : FSMState {

	[System.NonSerialized]
	public PlayerController Parent;

	float stunDuration;
	float startTime;

	Vector2 storedVelocity;

	// Use this for initialization
	public override void OnEnter ( object userData ) {
		
		Parent.playerAnimator.SetTrigger ("trap");
		Debug.Log( "Entered " + this );

		stunDuration = (float)userData;
		startTime = Time.time;

		storedVelocity = Parent.rigidbody2D.velocity;
		Parent.rigidbody2D.isKinematic = true;
	}

	// Use this for initialization
	public override void OnExit () {
		Debug.Log( "Exiting " + this );

		Parent.rigidbody2D.isKinematic = false;
		Parent.rigidbody2D.velocity = storedVelocity;
	}
	
	public override void OnUpdate(){

		//if we have been stunned long enough, go to previous state whatever that was...
		if ( Time.time > startTime + stunDuration ){
			Parent.GoToPreviousState();
		}
	}
}
