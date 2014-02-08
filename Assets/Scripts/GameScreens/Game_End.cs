using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_End : FSMState  {

	bool playerOneReady;
	bool playerTwoReady;
	bool winscreen;
	int winner;

	[System.NonSerialized]
	public GameController Parent;

	public override void OnEnter( object userData ){
		Debug.Log( "Entered " + this );
		Application.LoadLevel ("PostGame");

		playerOneReady = false;
		playerTwoReady = false;
		winner = (int) userData;
		Debug.Log ("Well done player " + userData);
		//GameObject.Instantiate( Resources.Load("Prefabs/Spray_Bad"), sprayBadPos, Quaternion.identity) ;

		winscreen = false;
		int winCondition = (int)userData;
		//show who wins.
		if (winCondition == 0) {
			//draw time 
		}
		//1 & 2 etc
	}

	public override void OnUpdate(){
		if (!winscreen) {
				GameObject.Instantiate (Resources.Load ("Prefabs/Win" + winner), Vector3.zero, Quaternion.identity);
			winscreen = true;
		}

		if (Input.GetButtonDown("Interact1") || Input.GetKeyDown("a")) {	//The a and b buttons ar just for testing
			playerOneReady = true;
		}
		if (Input.GetButtonDown("Interact2") || Input.GetKeyDown("b")) {
			playerTwoReady = true;
		}
		if ( playerOneReady && playerTwoReady  ){
			if (Parent.currentLevel < Parent.numberOfLevels){
				//go to next level...
				Parent.resetPosters();
				Parent.currentLevel = Parent.currentLevel + 1;
				Parent.GoToState(Parent.g_level_one);
				Application.LoadLevel("Level" + Parent.currentLevel);
			}
			else{
				Parent.resetGames();
				Parent.GoToState(Parent.g_menu);
				Application.LoadLevel("MainMenu");

			}

		}
	}
}
