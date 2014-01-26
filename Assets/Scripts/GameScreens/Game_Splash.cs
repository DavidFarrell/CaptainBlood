using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Splash : FSMState {

	[System.NonSerialized]
	public GameController Parent;
	public GUIText pressContinue;
	private bool guiActive = true;
	private float timer = 0;

	public override void OnEnter () {
		//GameObject newGameObject = GameObject.Instantiate (Resources.Load ("ScenePrefabs/ColinScenePrefab"), Vector3.zero, Quaternion.identity) as GameObject;

	}

	public override void OnUpdate(){
		timer += Time.deltaTime;
		if (timer > 1.3){
			if(guiActive){
				pressContinue.enabled = false;
				guiActive= false;
			}else{
				pressContinue.enabled = true;
				guiActive= true;
			}
			timer = 0;
		}

		if(Input.anyKeyDown){
			Debug.Log ("Splash: going to the next Level");
			//Parent.NextLevel();
			Parent.GoToState(Parent.g_menu);
			Application.LoadLevel("MainMenu");
		}
	}

}
