using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioSource : MonoBehaviour {

	void Awake()
	{
		// Make sure we only ever have one audio manager in the game. 
		if (!AudioManager.Initialized) 
		{
			// initialize audio manager and persist audio source across scenes
			AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
			AudioManager.Initialize (audioSource);
			DontDestroyOnLoad (gameObject);
		} else 
		{
			// destroy duplicate
			Destroy (gameObject);
		}
	}
}
