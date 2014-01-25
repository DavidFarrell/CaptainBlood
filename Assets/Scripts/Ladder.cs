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

		//Debug.Log ("Climbing ladder! vertAxis" + vertAxis);
		if(other.gameObject.tag == "Player"){		//Note that a negative vertical input means aiming up with the joystick! (it's weird but is like this...)
			pc = other.gameObject.GetComponent<PlayerController>();
			pc.vertAxis = Input.GetAxis(pc.VertInput());
			Debug.Log("Vertical input player " + pc.playerNumber.ToString() + ": " + pc.vertAxis);
			if(pc.vertAxis < 0.4){
				pc.GoToState(pc.s_ladder);
				Debug.Log ("Climbing ladder! vertAxis" + pc.vertAxis);
			}
		}
	}
	
	public void OnTriggerExit2D(Collider2D other){
		//if(other.gameObject.tag == "Ladder"){
		// Debug.Log ("Trigger exited");
			if(pc)	pc.GoToState(pc.s_fall);
		//}
	}
}
