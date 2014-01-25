using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Menu : FSMState {

	[System.NonSerialized]
	public GameController Parent;


	//GameObject newGameObject = GameObject.Instantiate (Resources.Load ("scenePrefab"), Vector3.zero, Quaternion.identity) as GameObject;
}
