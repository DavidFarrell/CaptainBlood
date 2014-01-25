using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Ladder: FSMState {

	[System.NonSerialized]
	public PlayerController Parent;

	private float horizSpeed;
	private float vertSpeed;
	private float gravityScaleBackup;

	// Use this for initialization
	public override void OnEnter () {
		Debug.Log( "Entered " + this );
		//Parent.gameObject.GetComponent <Rigidbody2D>().isKinematic = true;
		gravityScaleBackup = Parent.rigidbody2D.gravityScale;
		Parent.rigidbody2D.gravityScale = 0;//.isKinematic = true;
	}
	
	// Use this for initialization
	public override void OnExit () {
		Debug.Log( "Exiting " + this );
		//Parent.gameObject.GetComponent <Rigidbody2D>().isKinematic = false;
		Parent.rigidbody2D.gravityScale = gravityScaleBackup;//.isKinematic = false;
	}

	public override void OnUpdate(){
		//retrieve axis info
		Parent.vertAxis = Input.GetAxis(Parent.VertInput());
		Parent.horizAxis = Input.GetAxis(Parent.HorizInput());

		if (!Parent.canLadder) 
		{

			Parent.GoToState (Parent.s_fall);
		}

		//Parent.transform.rigidbody2D.velocity = new Vector2(Mathf.Sign( Parent.transform.rigidbody2D.velocity.x) * Parent.maxSpeed, Parent.transform.rigidbody2D.velocity.y);
		if (Mathf.Abs(Parent.horizAxis) > 0.1) { 
			horizSpeed = (float)(Mathf.Sign (Parent.horizAxis) * Parent.maxSpeed * 0.4);
		} else {
			horizSpeed = 0;
		}
		if(Mathf.Abs(Parent.vertAxis) > 0.1){
			vertSpeed = (float) (Mathf.Sign (Parent.vertAxis) * Parent.maxSpeed * -0.6);		//The vert axis is inverted. Incredible but true
		} else {
			vertSpeed = 0;
		}
		Parent.transform.rigidbody2D.velocity = new Vector2(horizSpeed, vertSpeed);

		if (Input.GetButtonDown(Parent.JumpInput())) {
			Parent.GoToState(Parent.s_jump);
		}
	}

}
