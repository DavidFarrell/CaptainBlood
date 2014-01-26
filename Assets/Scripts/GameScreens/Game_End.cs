using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_End : FSMState  {

	bool playerOneReady;
	bool playerTwoReady;

	[System.NonSerialized]
	public GameController Parent;

	public override void OnEnter( object userData ){
		Debug.Log( "Entered " + this );
		Application.LoadLevel ("PostGame");

		int winCondition = (int)userData;
		//show who wins.
		if (winCondition == 0) {
			//draw time 
		}
		//1 & 2 etc
	}

	public override void OnUpdate(){

		if ( playerOneReady && playerTwoReady ){

			//go to next level...

		}
	}
}
