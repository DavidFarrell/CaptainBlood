using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	PlayerController owner;
	public float stunDuration = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//fall until velocity etc is low enough to stop
		if ( rigidbody2D.velocity.magnitude < 0.1f ){
			//convert it into a trigger and remove the rigidbody...
			rigidbody2D.isKinematic = true;
			transform.collider2D.isTrigger = true;
		}
	}

	void OnTriggerEnter2D( Collider2D coll ) {
		Debug.Log( "SOMETHING HAS HIT TRAP! :O" );
		if ( coll.gameObject.tag == "Player"){
			if ( coll.gameObject.GetComponent<PlayerController>() != owner ){

				//remove trap!
				Destroy( gameObject );
				coll.gameObject.GetComponent<PlayerController>().GoToState( coll.gameObject.GetComponent<PlayerController>().s_stun, stunDuration );
			}
		}
	}
}
