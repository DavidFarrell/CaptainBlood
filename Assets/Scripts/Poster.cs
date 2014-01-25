using UnityEngine;
using System.Collections;

public class Poster : MonoBehaviour {

	public Sprite post1;
	public Sprite post2;
	public Sprite post3;
	
	private int state;
	
	private SpriteRenderer mySR;
	private PlayerController pc;

	// Use this for initialization
	void Start () {
		state = 0;
		mySR = gameObject.GetComponent<SpriteRenderer>();
		//testPoster();
	}
	
	// Update is called once per frame
	void Update () {
	
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
			mySR.sprite = post2;
			state = 1;
		break;
		case 2: 
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
