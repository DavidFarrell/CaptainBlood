﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Play : FSMState {

	[System.NonSerialized]
	public GameController Parent;

	public override void OnEnter(){
		Debug.Log ("Entering state: Game_Play");
		Application.LoadLevel ("MainGame");
	}

	public void OnUpdate(){
		
	}
}
