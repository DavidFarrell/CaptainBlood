using UnityEngine;
using System.Collections;

public class Spraypaint : MonoBehaviour {

	public float moveDistance = 0.1f;

	// Update is called once per frame
	void Update () {
	
		//jitter around in x & y within certain bounds...
		int direction = Random.Range( 0, 4 );

		switch ( direction ){

		case 0:

			transform.position = transform.position + new Vector3( -moveDistance, 0, 0 );

			break;

		case 1:

			transform.position = transform.position + new Vector3( moveDistance, 0, 0 );

			break;

		case 2:

			transform.position = transform.position + new Vector3( 0, moveDistance, 0 );

			break;

		case 3:

			transform.position = transform.position + new Vector3( 0, -moveDistance, 0 );

			break;

		};
	}
}
