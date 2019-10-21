using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ship.
/// </summary>
public class Ship : MonoBehaviour {

	//thrust support.
	Rigidbody2D shipRb2D; 
	Vector2 thrustDirection = new Vector2(1,0); 
	const float ThrustForce = 5;

	//Rotation support.
	const float RotateDegreePerSecond = 60;

	void Start(){
		shipRb2D = GetComponent<Rigidbody2D> ();
	}

	// FixedUpdate is called every fixed timed frame
	// We use it to apply a force on the vessel that is not dependent of the framerate. 
	void FixedUpdate(){
		if (Input.GetAxis ("Thrust") > 0) {
			shipRb2D.AddForce (Input.GetAxis("Thrust") * ThrustForce * thrustDirection, ForceMode2D.Force);
		}
	}

	// OnBecameInvisible is called when the gameobject is no longer visible by any camera.
	// We use this to wrap our world in a donut shape. 
	void OnBecameInvisible(){
        Debug.Log("save me");
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

	// Update is called once per frame
	// We listen for user input used to rotate the ship and the forward direction. 
	void Update () {
		if (Mathf.Abs (Input.GetAxis ("Rotate")) > 0) {
			float degrees = Input.GetAxis ("Rotate") * RotateDegreePerSecond * Time.deltaTime;
			transform.Rotate (Vector3.forward, degrees);
			float rotationAngle = transform.eulerAngles.z * Mathf.Deg2Rad;
			thrustDirection.x = Mathf.Cos (rotationAngle);
			thrustDirection.y = Mathf.Sin (rotationAngle);
		}

		if (Input.GetKeyUp(KeyCode.LeftControl)) {
			GameObject cannonBall = (GameObject)Instantiate (Resources.Load ("prefabs/cannon"));
			Physics2D.IgnoreCollision (cannonBall.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
			AudioManager.Play (AudioClipName.Cannon);
			cannonBall.transform.position = transform.position;
			cannonBall.GetComponent<Rigidbody2D> ().AddForce (thrustDirection * cannonBall.GetComponent<CannonBall>().Velocity, ForceMode2D.Impulse);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		GameObject explosion = (GameObject)Instantiate (Resources.Load ("prefabs/explosion"));
		explosion.transform.position = transform.position - new Vector3(0, 0.5f, 0);
		AudioManager.Play (AudioClipName.ShipWreck);
		Destroy (gameObject); 
	}


}
