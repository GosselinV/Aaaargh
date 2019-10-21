using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	GameTimer timer;
	[SerializeField]
	Text time; 
	const string timeString = "time : ";
	const string units = " seconds";
	// Use this for initialization
	void Start () {
		timer = GetComponent<GameTimer> ();
		time.text = timeString + timer.ElapsedTime.ToString () + units;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time.text = timeString + Mathf.Floor(timer.ElapsedTime).ToString () + units;
	}
}
