using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_End : FSMState  {

	bool playerOneReady;
	bool playerTwoReady;
	private GUIText gui1;
	private GUIText gui2;
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
				gui1 = GameObject.Find("GUI Text 1").GetComponent<GUIText>();
				gui2 = GameObject.Find("GUI Text 2").GetComponent<GUIText>();
			winscreen = true;
		}

		if (Input.GetButtonDown("Weapon1") ||Input.GetButtonDown("Jump1") || Input.GetButtonDown("Interact1") || Input.GetKeyDown("a")) {	//The a and b buttons ar just for testing
			playerOneReady = true;
			gui1.enabled = false;
		}
		if (Input.GetButtonDown("Weapon2") ||Input.GetButtonDown("Jump2") || Input.GetButtonDown("Interact2") || Input.GetKeyDown("b")) {
			playerTwoReady = true;
			gui2.enabled = false;
		}
		if ( playerOneReady && playerTwoReady  ){
			if (Parent.currentLevel < Parent.numberOfLevels){
				//go to next level...
				Parent.resetPosters();
				Parent.currentLevel++;
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
