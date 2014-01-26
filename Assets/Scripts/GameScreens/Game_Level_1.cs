using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Level_1 : FSMState {
	
	[System.NonSerialized]
	public GameController Parent;
	
	public override void OnEnter(){
		Debug.Log ("Entering state: Game_Level_1");
		Application.LoadLevel ("Level1");
	}
	
	public void OnUpdate(){
		
	}
}
