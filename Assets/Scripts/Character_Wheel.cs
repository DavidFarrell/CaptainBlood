﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Wheel : FSMState {
	
	[System.NonSerialized]
	public PlayerController Parent;
	public CircleCollider2D circle;
	private Vector3 vec1;
	private Vector3 newvec;

	public int soundIndex;
//	Vector2 storedVelocity;
	
	// Use this for initialization
	public override void OnEnter ( ) {
		//Debug.Log( "Entered " + this );
		
		Parent.playerAnimator.SetTrigger ("hanging");
		//	storedVelocity = Parent.rigidbody2D.velocity;
		Parent.rigidbody2D.isKinematic = true;
		soundIndex = Parent.speaker.playSound (AudioEngine.SOUND_POSTER_SCARED_SPINNING, true);

	}

	// Use this for initialization
	public override void OnExit () {
		//Debug.Log( "Exiting " + this );
		
		Parent.speaker.stopSound (soundIndex);
		Parent.speaker.playSound (AudioEngine.SOUND_POSTER_WHOOSH, false);

		Wheel wheel = Parent.transform.parent.GetComponent<Wheel>();


		Parent.rigidbody2D.isKinematic = false;
		circle = Parent.transform.parent.GetComponent<CircleCollider2D>();
		vec1 =  Parent.transform.parent.position - Parent.transform.position;
		newvec = Vector3.Cross (vec1, Vector3.forward);
		newvec.Normalize();

		Parent.rigidbody2D.velocity = newvec * wheel.wheelChuck;
		if (Parent.transform.parent.GetComponent<Wheel> ().rotationSpeed < 0.0f) {
			Parent.rigidbody2D.velocity = Parent.rigidbody2D.velocity * -1.0f;
		}

		Parent.transform.parent = null;
		//Parent.rigidbody2D.velocity = storedVelocity;
	}
	
	public override void OnUpdate(){
		//Cancel out the wheel rotation

		Parent.transform.Rotate (0.0f, 0.0f, -Parent.transform.parent.GetComponent<Wheel>().rotationSpeed);
		Debug.Log (Parent.transform.rigidbody2D.velocity.x);
		if (Input.GetButton (Parent.InteractInput ()) == false) {


			Parent.GoToState( Parent.s_fall );
		}
	}
}
