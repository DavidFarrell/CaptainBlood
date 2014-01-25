using UnityEngine;
using System.Collections;

public class PushableLadder : MonoBehaviour {
	
	public void Push( float horizAxis ){

		rigidbody2D.AddForce( new Vector2( horizAxis * 1000.0f, 0 ) );

	}
}
