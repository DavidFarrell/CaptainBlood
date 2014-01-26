using UnityEngine;
using System.Collections;

public class Poster : MonoBehaviour {

	public Sprite post1;
	public Sprite post2;
	public Sprite post3;
	
	private int state = 0;
	
	private SpriteRenderer mySR;
	private PlayerController pc;

	GameController gc;

	// Use this for initialization
	void Start () {
		mySR = gameObject.GetComponent<SpriteRenderer>();
		//testPoster();
	}
	
	// Update is called once per frame
	void Update () {
		if ( gc == null ){
			gc = GameObject.FindGameObjectWithTag( "GameController" ).GetComponent<GameController>();
		}
	
	}

	public void ChangePoster(int poster){
		Debug.Log ("poster change" + poster);

		//Changes the poster to display to the one specified on the parameter 'poster'
		switch (poster){
		case 0:
			mySR.sprite = post1;
			state = 0;
		break;
		case 1:
			gc.AdjustBlue(1);
			if ( state == 2 ){
				gc.AdjustRed( -1 );
			}
			Vector3 sprayGoodPos = transform.position + Vector3.back;
			GameObject.Instantiate( Resources.Load("Prefabs/Spray_Good"), sprayGoodPos, Quaternion.identity) ;
			mySR.sprite = post2;
			state = 1;
		break;
		case 2:
			gc.AdjustRed( 1 );
			if ( state == 1 ){
				gc.AdjustBlue( -1 );
			}
			Vector3 sprayBadPos = transform.position + Vector3.back;
			GameObject.Instantiate( Resources.Load("Prefabs/Spray_Bad"), sprayBadPos, Quaternion.identity) ;
			mySR.sprite = post3;
			state = 2;
		break;
		default: 
			Debug.Log("There's only 3 posters, from 0 to 2. Tried to access " + poster);
		break;
		}
	}

	public int GetState(){
		//Returns the actual state of the poster
		return state;
	}
	/*
	public void OnTriggerEnter2D(Collider2D other) {
		
		Debug.Log ("Entering Poster: " + other.gameObject.tag);
		if(other.gameObject.tag == "Player"){		//Note that a negative vertical input means aiming up with the joystick! (it's weird but is like this...)
			pc = other.gameObject.GetComponent<PlayerController>();
			pc.facedPoster = this;
			Debug.Log (pc.facedPoster.gameObject.name);
		}
	}
	
	public void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Exiting Poster");
		if(other.gameObject.tag == "Player"){
			pc = other.gameObject.GetComponent<PlayerController>();
			pc.facedPoster = null;
		}
	}*/

}
