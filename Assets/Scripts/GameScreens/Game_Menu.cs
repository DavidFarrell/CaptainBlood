using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Menu : FSMState {

	[System.NonSerialized]
	public GameController Parent;
	private GUIText gui1;
	private GUIText gui2;
	private bool firstTime = true;

	public override void OnEnter(){
		Debug.Log ("Level MainMenu loaded? " + Application.loadedLevelName);
		firstTime = true;
	}
	//GameObject newGameObject = GameObject.Instantiate (Resources.Load ("scenePrefab"), Vector3.zero, Quaternion.identity) as GameObject;

	public override void OnUpdate(){
		if (firstTime){

			gui1 = GameObject.Find("GUI Text 1").GetComponent<GUIText>();
			gui2 = GameObject.Find("GUI Text 2").GetComponent<GUIText>();
			firstTime = false;
		}

		if (Input.GetButtonDown("Weapon1") ||Input.GetButtonDown("Jump1") || Input.GetButtonDown("Interact1") || Input.GetKeyDown("a")) {	//The a and b buttons ar just for testing
			gui1.enabled = false;
		}
		if (Input.GetButtonDown("Weapon2") ||Input.GetButtonDown("Jump2") || Input.GetButtonDown("Interact2") || Input.GetKeyDown("b")) {
			gui2.enabled = false;
		}
		if (  !gui1.enabled && !gui2.enabled){
			//Parent.NextLevel();
			Parent.GoToState(Parent.g_level_one);
			Application.LoadLevel("Level1");

		}
	}
}
