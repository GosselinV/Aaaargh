using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

	float velocity = 10f;
	Timer timer; 

	public float Velocity{
		get { return velocity; }
	}

	void Start(){
		foreach (GameObject cannonBalls in GameObject.FindGameObjectsWithTag("cannonball")){
			Physics2D.IgnoreCollision (cannonBalls.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
		
		}

		timer = GetComponent<Timer>(); 
		timer.Duration = 2f; 
		timer.Run ();
	}

	void OnBecameInvisible(){		
		Vector3 position = transform.position;
		if (position.x >= ScreenUtils.ScreenRight) {
			position.x = ScreenUtils.ScreenLeft;
		} else if (position.x <= ScreenUtils.ScreenLeft) {
			position.x = ScreenUtils.ScreenRight;
		} 
		if (position.y >= ScreenUtils.ScreenTop) {
			position.y = ScreenUtils.ScreenBottom;
		} else if (position.y <= ScreenUtils.ScreenBottom) {
			position.y = ScreenUtils.ScreenTop;
		}
		transform.position = position; 
	}

	void Update(){

		if (timer.Finished) {
			Destroy (gameObject); 
		}
	}
}
