using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour {


	public float timeToDestroy = 5;
	float spawnTime;

	
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		Destroy (gameObject, timeToDestroy);
	}
}	

