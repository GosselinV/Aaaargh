using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	#region field

	static bool initialized = false;
	static AudioSource audioSource;
	static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip> ();

	#endregion

	#region Properties

	/// <summary>
	/// Gets Whether or not the audio manager has been initialized.
	/// </summary>
	public static bool Initialized{

		get { return initialized; }
	}

	#endregion


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void Initialize (AudioSource source)
	{
		initialized = true;
		audioSource = source;
		audioClips.Add (AudioClipName.Cannon, Resources.Load<AudioClip> ("audio/audioClips/Cannon"));
		audioClips.Add (AudioClipName.SharkSplit, Resources.Load<AudioClip> ("audio/audioClips/SharkSplit"));
		audioClips.Add (AudioClipName.ShipWreck, Resources.Load<AudioClip> ("audio/audioClips/ShipWreck"));
		audioClips.Add (AudioClipName.WilhelmScream, Resources.Load<AudioClip> ("audio/audioClips/WilhelmScream"));
	}

	public static void Play(AudioClipName name){
		audioSource.PlayOneShot (audioClips [name]);
	}
}