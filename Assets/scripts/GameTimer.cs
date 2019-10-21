using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour 
{
	
	float elapsedTime = 0f;

	public float ElapsedTime{
		get { return elapsedTime; }
	}

	void Update(){
		print ("hi");
		elapsedTime += Time.deltaTime;
	}

}


