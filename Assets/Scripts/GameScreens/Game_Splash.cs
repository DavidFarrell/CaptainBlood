using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Splash : FSMState {

	[System.NonSerialized]
	public GameController Parent;
	public GUIText pressContinue;
	private bool guiActive = true;
	private float timer = 0;
	public AudioEngine speaker;
	private int tune;

	public override void OnEnter () {
		//GameObject newGameObject = GameObject.Instantiate (Resources.Load ("ScenePrefabs/ColinScenePrefab"), Vector3.zero, Quaternion.identity) as GameObject;
		tune = 999;
	}

	public override void OnUpdate(){
		if (tune == 999) {
			tune = speaker.playSound (AudioEngine.SOUND_POSTER_ATTRACT_MODE, true);
		}
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
			speaker.stopSound(tune);
			Parent.GoToState(Parent.g_menu);
			Application.LoadLevel("MainMenu");
		}
	}

}
