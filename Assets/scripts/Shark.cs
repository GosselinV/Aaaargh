using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {

	#region fields

	SharkSpriteName spriteName; 
	Vector2 velocity;

	#endregion

	#region properties

	public SharkSpriteName SpriteName{

		set {spriteName = value;}
		get {return spriteName;}

	}

	public Vector2 Velocity{

		set {velocity = value; 
			GetComponent<Rigidbody2D> ().AddForce (value, ForceMode2D.Impulse); }
		get {return velocity;} 
	}

	#endregion

	// Set velocity
//	public void SetVelocity (Vector3 direction, float magnitude) {
//		GetComponent<Rigidbody2D>().AddForce(
//			direction * magnitude,
//			ForceMode2D.Impulse);
//	}

	#region methods

	void Start(){
		foreach (GameObject sharks in GameObject.FindGameObjectsWithTag("shark")) {
			Physics2D.IgnoreCollision (sharks.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
		}
	}

	// Screen Wrapping
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

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "cannonball") {
			Destroy (col.gameObject);
			ResolveCannon ();
			Destroy (gameObject);
		} else if (col.gameObject.tag == "ship") {
			AudioManager.Play (AudioClipName.ShipWreck);
			Explosion ();
			Destroy (col.gameObject);
		}
	}

	void Explosion(){
		GameObject explosion = (GameObject)Instantiate (Resources.Load ("prefabs/explosion"));
		explosion.transform.position = transform.position - new Vector3 (0, 0.5f, 0);
	}

	void ResolveCannon()
	{
		List<Vector2> velocity = getNewVelocity ();
		print (velocity[0]);
		print (velocity [1]);
		Camera cam = Camera.main;
		print (SpriteName);
		if (SpriteName == SharkSpriteName.shark1) {
			AudioManager.Play (AudioClipName.SharkSplit);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [0]), SharkSpriteName.halfShark1, velocity [0]);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [1]), SharkSpriteName.halfShark1, velocity [1]);
		} else if (spriteName == SharkSpriteName.shark2) {
			AudioManager.Play (AudioClipName.SharkSplit);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [0]), SharkSpriteName.halfShark2, velocity [0]);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [1]), SharkSpriteName.halfShark2, velocity [1]);
		} else if (spriteName == SharkSpriteName.shark3) {
			AudioManager.Play (AudioClipName.SharkSplit);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [0]), SharkSpriteName.halfShark3, velocity [0]);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [1]), SharkSpriteName.halfShark3, velocity [1]);
		} else if (spriteName == SharkSpriteName.halfShark1) {
			AudioManager.Play (AudioClipName.SharkSplit);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [0]), SharkSpriteName.quarterShark1, velocity [0]);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [1]), SharkSpriteName.quarterShark1, velocity [1]);
		} else if (spriteName == SharkSpriteName.halfShark2) {
			AudioManager.Play (AudioClipName.SharkSplit);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [0]), SharkSpriteName.quarterShark2, velocity [0]);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [1]), SharkSpriteName.quarterShark2, velocity [1]);
		} else if (spriteName == SharkSpriteName.halfShark3) {
			AudioManager.Play (AudioClipName.SharkSplit);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [0]), SharkSpriteName.quarterShark3, velocity [0]);
			cam.GetComponent<SharkSpawner> ().SpawnShark (getNewLocation (velocity [1]), SharkSpriteName.quarterShark3, velocity [1]);
		} else {
			AudioManager.Play (AudioClipName.WilhelmScream);
		}
	}

	List<Vector2> getNewVelocity(){
		Vector2 perpVelocity = new Vector2 (velocity.y, -velocity.x);
		List<Vector2> newVelocity = new List<Vector2> ();
		newVelocity.Add (velocity.normalized + perpVelocity.normalized);
		newVelocity.Add (velocity.normalized - perpVelocity.normalized);
		return newVelocity;
	}

	Vector3 getNewLocation(Vector2 velocity){
		Vector3 direction = new Vector3 (velocity.x, velocity.y, 0);
	    return transform.position + gameObject.GetComponent<CircleCollider2D> ().radius * direction.normalized;

	}

	#endregion
}
