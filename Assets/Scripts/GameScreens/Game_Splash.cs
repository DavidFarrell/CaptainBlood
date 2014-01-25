using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Splash : FSMState {

	[System.NonSerialized]
	public GameController Parent;

	public override void OnEnter () {
		GameObject newGameObject = GameObject.Instantiate (Resources.Load ("ScenePrefabs/ColinScenePrefab"), Vector3.zero, Quaternion.identity) as GameObject;
	}
}
