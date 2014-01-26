using UnityEngine;
using System.Collections;

public class AudioEngine : MonoBehaviour {
	
	// STATIC VARS

	public static int SOUND_WALKING = 0;
	public static int SOUND_JUMP = 1;
	public static int SOUND_LAND = 2;
	public static int SOUND_TRAP_THROW = 3;
	public static int SOUND_TRAP_LAND = 4;
	public static int SOUND_POSTER_BADDIE = 5;
	public static int SOUND_POSTER_GOODIE = 6;
	public static int SOUND_POSTER_STUN = 7;
	public static int SOUND_POSTER_LADDER_WHEELS = 8;
	public static int SOUND_POSTER_LAND_NOISE = 9;
	public static int SOUND_POSTER_PINBALL = 10;
	public static int SOUND_POSTER_SCARED_SPINNING=12;
	public static int SOUND_POSTER_SKY_REEL = 11;
	public static int SOUND_POSTER_WHOOSH = 13;
	public static int SOUND_POSTER_LADDER = 14;
	public static int SOUND_POSTER_ATTRACT_MODE = 15;
	
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
