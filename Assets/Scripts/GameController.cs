using UnityEngine;
using System.Collections;

public class GameController : FSMSystem {
	
	//Add the states
	public Game_Menu g_menu;
	public Game_Splash g_splash;
	//public Game_Play g_play;
	public Game_Level_1 g_level_one;
	public Game_End g_end;

	void Awake(){

		DontDestroyOnLoad(transform.gameObject);	//This will make this object survive between scenes

		AddState( g_menu );
		g_menu.Parent = this;

		AddState (g_splash);
		g_splash.Parent = this;

		AddState (g_level_one);
		g_level_one.Parent = this;

		AddState (g_end);
		g_end.Parent = this;
		
		//goto first default state
		GoToState( g_splash );
	}

	public void NextLevel(){
		//This function has to be called at the end of each level, to go to the following level or end the game.
		Debug.Log ("NextLevel(): " + this.CurrentState.ToString());
		switch(this.CurrentState.ToString()){
		case "Game_Splash": 				//The name of the STATE associated with the scene, not the scene!!!
			Debug.Log ("Changing level to: " + g_menu.ToString());
			GoToState(g_menu);
			break;
		case "Game_Menu": 
			Debug.Log ("Changing level to: " + g_level_one.ToString());
			GoToState(g_level_one);
			break;
		case "Game_Level_1": 
			Debug.Log ("Changing level to: " + g_end.ToString());
			GoToState(g_end);
			break;
		case "g_end": 
			Debug.Log ("Exiting game");
			Application.Quit();
			break;
		default:
			Debug.Log ("GameController: Name of level not found!");
			break;
		}
	}


}
