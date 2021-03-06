﻿using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {
	
	PlayerController pc;
	//CircleCollider2D circle;
	
	public float wheelChuck;
	public float rotationSpeed;
	public AudioEngine speaker;
	public int soundIndex;

	void Update () {
		transform.Rotate (0.0f, 0.0f, rotationSpeed);
	}

	void Start() {
		soundIndex = speaker.playSound (AudioEngine.SOUND_POSTER_SKY_REEL, true);
	}

/*	public void OnTriggerEnter2D(Collider2D other) {
		
		if(other.gameObject.tag == "Player"){		
			pc = other.gameObject.GetComponent<PlayerController>();

			if (Input.GetButtonDown (pc.InteractInput ())) {
				other.transform.parent = transform;
				pc = other.gameObject.GetComponent<PlayerController>();
				//circle = GetComponent<CircleCollider2D>();
				pc.GoToState(pc.s_wheel);
			}
		//	other.gameObject.GetComponent<PlayerController>().GoToState( other.gameObject.GetComponent<PlayerController>().s_wheel );
				
		}
	}
	
	public void OnTriggerExit2D(Collider2D other){
		pc = null;
	}*/

	public void Grab(PlayerController pc){
		Debug.Log ("I was grabbed!");
		pc.transform.parent = transform;
		pc.GoToState(pc.s_wheel);
	}


}
