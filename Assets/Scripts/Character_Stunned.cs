using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Stunned : FSMState {

	[System.NonSerialized]
	public PlayerController Parent;

	float stunDuration;
	float startTime;

	// Use this for initialization
	public override void OnEnter ( object userData ) {
		Debug.Log( "Entered " + this );

		stunDuration = (float)userData;
	}

	// Use this for initialization
	public override void OnExit () {
		Debug.Log( "Exiting " + this );
	}
	
	public override void OnUpdate(){

		//if we have been stunned long enough, go to previous state whatever that was...
		if ( Time.time > startTime + stunDuration ){
			Parent.GoToPreviousState();
		}
	}
}
