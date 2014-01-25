﻿using UnityEngine;
using System.Collections;

public class PlayerController : FSMSystem {
	
	//Add the states
	public Character_Idle s_idle;
	public Character_Walk s_walk;
	public Character_Jump s_jump;
	public Character_Falling s_fall;
	public Character_Ladder s_ladder;
	public Character_Stunned s_stun;
	public Character_Interact s_interact;
	
	public bool facingRight = true;
	public bool grounded;
	public float horizAxis;
	public float moveForce;
	public float jumpForce;
	public float maxSpeed;	

	public float playerNumber;
	public Transform playerGrounder;


	public AudioClip[] aClips;

	
	void Awake(){
		AddState( s_idle );
		s_idle.Parent = this;
		AddState (s_walk);
		s_walk.Parent = this;
		AddState (s_jump);
		s_jump.Parent = this;
		AddState( s_fall );
		s_fall.Parent = this;
		AddState( s_ladder );
		s_ladder.Parent = this;
		AddState( s_stun );
		s_stun.Parent = this;
		AddState( s_interact );
		s_interact.Parent = this;

		//goto first default state
		GoToState( s_idle );
	}

	public string HorizInput(){
		return "Horizontal" + playerNumber.ToString();
	}

	public string JumpInput(){
		return "Jump" + playerNumber.ToString();
	}



	/// <summary>
	/// Flip the direction character is facing
	/// </summary>
	public void Flip(){
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}


	public void LineCasting(){
		Debug.DrawLine (transform.position, playerGrounder.position, Color.cyan);
		grounded = Physics2D.Linecast (transform.position, playerGrounder.position, 1 << LayerMask.NameToLayer ("Middleground"));
	}

	public void playFootsteps() {
		if (!audio.isPlaying) {
			audio.clip = aClips [0];
			audio.Play ();
		}

	}
}
