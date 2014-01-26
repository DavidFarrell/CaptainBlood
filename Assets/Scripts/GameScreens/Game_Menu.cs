﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Menu : FSMState {

	[System.NonSerialized]
	public GameController Parent;
	private GUIText gui1;
	private GUIText gui2;
	private bool firstTime = true;

	public override void OnEnter(){
		Application.LoadLevel ("MainMenu");
		Debug.Log ("Level MainMenu loaded? " + Application.loadedLevelName);
		//while (!Application.loadedLevelName == "MainMenu"); )
	}
	//GameObject newGameObject = GameObject.Instantiate (Resources.Load ("scenePrefab"), Vector3.zero, Quaternion.identity) as GameObject;

	public override void OnUpdate(){
		if (firstTime){
			firstTime = false;
			gui1 = GameObject.Find("GUI Text 1").GetComponent<GUIText>();
			gui2 = GameObject.Find("GUI Text 2").GetComponent<GUIText>();
		}

		if (Input.GetButtonDown("Interact1") || Input.GetKeyDown("a")) {	//The a and b buttons ar just for testing
			gui1.enabled = false;
		}
		if (Input.GetButtonDown("Interact2") || Input.GetKeyDown("b")) {
			gui2.enabled = false;
		}
		if (!gui1.enabled && !gui2.enabled){
			//Parent.NextLevel();
			Parent.GoToState(Parent.g_level_one);
			Application.LoadLevel("Level1");

		}
	}
}
