using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour {

	public int frequency;
	float spawnTime;

	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		GameObject newPickup = GameObject.Instantiate ( Resources.Load ("Prefabs/Pickup"), transform.position, Quaternion.identity) as GameObject;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > spawnTime + frequency){
			//spawn a pickup
			GameObject newPickup = GameObject.Instantiate ( Resources.Load ("Prefabs/Pickup"), transform.position, Quaternion.identity) as GameObject;
			//reset timer
			spawnTime = Time.time;

			
			
		}
	
	}
}
