using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Falling : FSMState {
	
	[System.NonSerialized]
	public PlayerController Parent;
	
	// Use this for initialization
	public override void OnEnter () {
		Debug.Log( "Entered " + this );
	}
	
	// Use this for initialization
	public override void OnExit () {
		Debug.Log( "Exiting " + this );
	}
	
	public override void OnUpdate(){
		
	}
	
	
}
