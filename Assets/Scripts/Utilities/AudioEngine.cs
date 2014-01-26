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
	public bool[] leaveAlone;

	public int numSpeakers = 20;
	public int currentSpeaker = 0;

	// Use this for initialization
	void Start () {
		speakers = new GameObject[numSpeakers];
		leaveAlone = new bool[numSpeakers];

		for (int i = 0; i < numSpeakers; i++) {
			GameObject gameObject = new GameObject();
			gameObject.AddComponent("AudioSource");
			speakers[i] = gameObject;
			leaveAlone[i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int playSound(int sound, bool loop = false) {
		int thisSpeaker = currentSpeaker;
		GameObject gameObject = speakers [currentSpeaker];
		gameObject.audio.clip = sounds [sound];
		gameObject.audio.loop = loop;
		gameObject.audio.Play ();

		// for footsteps and other things that can't be overwritten
		if (loop) {
			leaveAlone [currentSpeaker] = true;
		}

		currentSpeaker++;
		if (currentSpeaker >= numSpeakers) {
			currentSpeaker = 0;
		}

		// if there's a looper, leave it alone!
		while (leaveAlone[currentSpeaker]) {
			currentSpeaker++;
			if (currentSpeaker >= numSpeakers) {
				currentSpeaker = 0;
			}
		}

		return thisSpeaker;
	}

	public void stopSound(int speakerIndex) {
		GameObject gameObject = speakers [speakerIndex];
		gameObject.audio.loop = false;
		gameObject.audio.Stop ();
		leaveAlone [speakerIndex] = false;
	}
}
