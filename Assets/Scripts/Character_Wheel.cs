using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character_Wheel : FSMState {
	
	[System.NonSerialized]
	public PlayerController Parent;
	public CircleCollider2D circle;
	private Vector3 vec1;
	private Vector3 newvec;


//	Vector2 storedVelocity;
	
	// Use this for initialization
	public override void OnEnter ( ) {
		Debug.Log( "Entered " + this );

		//	storedVelocity = Parent.rigidbody2D.velocity;
			Parent.rigidbody2D.isKinematic = true;

	}
	
	// Use this for initialization
	public override void OnExit () {
		Debug.Log( "Exiting " + this );
		Wheel wheel = Parent.transform.parent.GetComponent<Wheel>();


		Parent.rigidbody2D.isKinematic = false;
		circle = Parent.transform.parent.GetComponent<CircleCollider2D>();
		vec1 =  Parent.transform.parent.position - Parent.transform.position;
		newvec = Vector3.Cross (vec1, Vector3.forward);
		newvec.Normalize();
		Parent.rigidbody2D.velocity = newvec * wheel.wheelChuck;
		Parent.transform.parent = null;
		//Parent.rigidbody2D.velocity = storedVelocity;
	}
	
	public override void OnUpdate(){
		//Cancel out the wheel rotation
		Parent.transform.Rotate (0.0f, 0.0f, -1.0f);

		if (Input.GetButton (Parent.InteractInput ()) == false) {


			Parent.GoToState( Parent.s_fall );
		}
	}
}
