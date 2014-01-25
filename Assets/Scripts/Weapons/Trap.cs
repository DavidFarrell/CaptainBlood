using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	public PlayerController owner;
	public float stunDuration = 1.0f;

	bool activated = false;
	float startTime;
	float gracePeriod = 1.0f;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		Physics2D.IgnoreLayerCollision( gameObject.layer, owner.gameObject.layer );
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( rigidbody2D.velocity.sqrMagnitude < 0.01f && Time.time > startTime + gracePeriod ){
			activated = true;
		}

		//fall until velocity etc is low enough to stop
		if ( activated ){
			//convert it into a trigger and remove the rigidbody...
			rigidbody2D.isKinematic = true;
			transform.collider2D.isTrigger = true;
		}
	}

	void OnTriggerEnter2D( Collider2D coll ) {
		Debug.Log( "SOMETHING HAS HIT TRAP! :O" );

		if ( activated ){
			if ( coll.gameObject.tag == "Player"){
				if ( coll.gameObject.GetComponent<PlayerController>() != owner ){
		
					//remove trap!
					Destroy( gameObject );
					coll.gameObject.GetComponent<PlayerController>().GoToState( coll.gameObject.GetComponent<PlayerController>().s_stun, stunDuration );
				
				}
			}
		}
	}
}
