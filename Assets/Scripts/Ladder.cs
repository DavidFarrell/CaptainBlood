using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	PlayerController pc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("hitting ladder ");
		if(other.gameObject.tag == "Player"){
			pc = other.gameObject.GetComponent<PlayerController>();
			Debug.Log ("facingLadder");
			pc.facingLadder = true;
		}
	}
	
	public void OnTriggerStay2D(Collider2D other) {
		Debug.Log ("OnTriggerStay ");
		if(other.gameObject.tag == "Player"){		//Note that a negative vertical input means aiming up with the joystick! (it's weird but is like this...)
			pc = other.gameObject.GetComponent<PlayerController>();
			pc.vertAxis = Input.GetAxis(pc.VertInput());
			Debug.Log("OnTriggerStay player");
			//Debug.Log("Vertical input player " + pc.playerNumber.ToString() + ": " + pc.vertAxis);
			if(pc.vertAxis < (-0.3)){
				pc.GoToState(pc.s_ladder);
				Debug.Log ("Climbing ladder! vertAxis" + pc.vertAxis);
			}
		}
	}
	
	public void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {		//Note that a negative vertical input means aiming up with the joystick! (it's weird but is like this...)
			pc = other.gameObject.GetComponent<PlayerController> ();
			pc.vertAxis = Input.GetAxis (pc.VertInput ());
			Debug.Log ("not facingLadder");
			pc.facingLadder = false;
			if(!pc.grounded){
				pc.GoToState(pc.s_fall);
			}else{
				pc.GoToState(pc.s_walk);
			}
		}
	}
}
