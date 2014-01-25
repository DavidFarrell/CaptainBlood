using UnityEngine;
using System.Collections;

public class PlayerController : FSMSystem {
	
	//Add the states
	public Character_Idle s_idle;
	public Character_Walk s_walk;
	
	public bool facingRight = true;
	public bool grounded;
	public float horizAxis;
	public float moveForce;
	public float jumpForce;
	public float maxSpeed;	
	public float playerNumber;
	public Transform playerGrounder;


	
	void Awake(){
		
		AddState( s_idle );
		s_idle.Parent = this;
		
		AddState (s_walk);
		s_walk.Parent = this;
		
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
}
