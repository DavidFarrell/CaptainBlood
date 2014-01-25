using UnityEngine;
using System.Collections;

public class GameController : FSMSystem {
	
	//Add the states
	public Game_Menu g_menu;
	public Game_Splash g_splash;
	public Game_Play g_play;
	public Game_End g_end;

	void Awake(){
		AddState( g_menu );
		g_menu.Parent = this;
		AddState (g_splash);
		g_splash.Parent = this;
		AddState (g_play);
		g_play.Parent = this;
		AddState (g_end);
		g_play.Parent = this;
		
		//goto first default state
		GoToState( g_splash );
	}




}
