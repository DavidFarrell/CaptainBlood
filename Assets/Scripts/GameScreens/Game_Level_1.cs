using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Level_1 : FSMState {
	
	[System.NonSerialized]
	public GameController Parent;

	public float maximumTime = 180.0f;
	float startTime;

	public int maxPosters;
	

	public override void OnEnter(){
		Debug.Log ("Entering state: Game_Level_1");
		Application.LoadLevel ("Level"+Parent.currentLevel);

		startTime = Time.time;
	}
	
	public override void OnUpdate(){

		//check there is still time on the clock
		if ( Time.time > startTime + maximumTime ){
			Parent.GoToState( Parent.g_end );
		}
	}


}
