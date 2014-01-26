using UnityEngine;
using System.Collections;

public class AudioEngine : MonoBehaviour {
	
	// STATIC VARS

	public static int SOUND_WALKING = 0;
	public static int SOUND_JUMP = 1;
	public static int SOUND_LAND = 2;
	public static int SOUND_TRAP_THROW = 3;
	public static int SOUND_TRAP_LAND = 4;
	public static int SOUND_POSTER_POST = 5;
	public static int SOUND_POSTER_SPRAY = 6;
	
	public GameObject[] speakers;
	public AudioClip[] sounds;

	public int numSpeakers = 20;
	public int currentSpeaker = 0;

	// Use this for initialization
	void Start () {
		speakers = new GameObject[numSpeakers];

		for (int i = 0; i < numSpeakers; i++) {
			GameObject gameObject = new GameObject();
			gameObject.AddComponent("AudioSource");
			speakers[i] = gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void playSound(int sound) {

		GameObject gameObject = speakers [currentSpeaker];
		gameObject.audio.clip = sounds [sound];
		gameObject.audio.Play ();
		currentSpeaker++;
		Debug.Log ("current speaker" + currentSpeaker);
		if (currentSpeaker >= numSpeakers) {
			currentSpeaker = 0;
		}
	}
}
