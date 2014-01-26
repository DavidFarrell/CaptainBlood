using UnityEngine;
using System.Collections;

public class SparksRotator : MonoBehaviour {

	public float rotationSpeed = 10.0f;

	// Update is called once per frame
	void Update () {
	
		transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
	}
}
