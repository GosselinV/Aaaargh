using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour {
	Dictionary<SharkSpriteName, Sprite> sharkSprites = new Dictionary<SharkSpriteName, Sprite> ();
	Dictionary<SharkSpriteName, Sprite> halfSharkSprites = new Dictionary<SharkSpriteName, Sprite> ();
	Dictionary<SharkSpriteName, Sprite> quarterSharkSprites = new Dictionary<SharkSpriteName, Sprite> ();
	GameObject sharkPrefab;

	const float speed = 1f; 

	void Start(){

		//spawn free location support
		sharkPrefab = (GameObject)Instantiate(Resources.Load("prefabs/shark"));
		float cameraMainTransformZ = -Camera.main.transform.position.z;
		float sharkColliderRadius = sharkPrefab.GetComponent<CircleCollider2D> ().radius;
		float sharkMinSpawnX = ScreenUtils.ScreenLeft + sharkColliderRadius;
		float sharkMaxSpawnX = ScreenUtils.ScreenRight - sharkColliderRadius;
		float sharkMinSpawnY = ScreenUtils.ScreenBottom + sharkColliderRadius;
		float sharkMaxSpawnY = ScreenUtils.ScreenTop - sharkColliderRadius;

		// No need for the original prefab anymore.
		Destroy (sharkPrefab);

		List<Vector3> sharkSpawnLocations = new List<Vector3> ();
		//sharkSpawnLocations.Add (new Vector3 (Random.Range(ScreenUtils.ScreenRight ,ScreenUtils.ScreenLeft), ScreenUtils.ScreenBottom, cameraMainTransformZ));
	    //sharkSpawnLocations.Add (new Vector3 (ScreenUtils.ScreenRight, Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop), cameraMainTransformZ));
		//sharkSpawnLocations.Add (new Vector3 (ScreenUtils.ScreenLeft, Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop), cameraMainTransformZ));
		sharkSpawnLocations.Add (new Vector3 (Random.Range(ScreenUtils.ScreenRight, ScreenUtils.ScreenLeft), ScreenUtils.ScreenTop, cameraMainTransformZ));

        // Populate sharkSprites dictionnary
		sharkSprites.Add(SharkSpriteName.shark1, Resources.Load<Sprite> ("sprites/shark1"));
		sharkSprites.Add(SharkSpriteName.shark2, Resources.Load<Sprite> ("sprites/shark2"));
		sharkSprites.Add(SharkSpriteName.shark3, Resources.Load<Sprite> ("sprites/shark3"));
		sharkSprites.Add(SharkSpriteName.halfShark1, Resources.Load<Sprite> ("sprites/halfShark1"));
		sharkSprites.Add(SharkSpriteName.halfShark2, Resources.Load<Sprite> ("sprites/halfShark2"));
		sharkSprites.Add(SharkSpriteName.halfShark3, Resources.Load<Sprite> ("sprites/halfShark3"));
		sharkSprites.Add(SharkSpriteName.quarterShark1, Resources.Load<Sprite> ("sprites/quarterShark1"));
		sharkSprites.Add(SharkSpriteName.quarterShark2, Resources.Load<Sprite> ("sprites/quarterShark2"));
		sharkSprites.Add(SharkSpriteName.quarterShark3, Resources.Load<Sprite> ("sprites/quarterShark3"));

		//spawn all intial sharks
		foreach (Vector3 sharkSpawnLocation in sharkSpawnLocations) 
		{
			// Randomize shark Sprite
			Vector2 velocity = new Vector2(-sharkSpawnLocation.x, -sharkSpawnLocation.y);
			SpawnShark (sharkSpawnLocation, getSharkSprite(Random.Range (0, 3)), speed*velocity);

		}

	}

	public void SpawnShark(Vector3 location, SharkSpriteName spriteName, Vector2 velocity){
		// Instantiate shark at random spawn location with random Sprite
		GameObject shark = (GameObject)Instantiate (Resources.Load("prefabs/shark"));
		shark.transform.position = location;
		//shark.GetComponent<Shark> ().SetVelocity (new Vector3 (-location.x, -location.y, 0), 0.2f);
		shark.GetComponent<Shark>().Velocity = speed*velocity.normalized;
		shark.GetComponent<Shark> ().SpriteName = spriteName;
		SpriteRenderer sharkSpriteRenderer = shark.GetComponent<SpriteRenderer> ();
		sharkSpriteRenderer.sprite = sharkSprites[spriteName];
	}

	//returns a sharkspritename to be used in dictionary. 
	SharkSpriteName getSharkSprite(int rand)
	{
		switch (rand) {
		case(0):
			return SharkSpriteName.shark1;
			break;
		case(1):
			return SharkSpriteName.shark2;
			break;
		default:
			return SharkSpriteName.shark3;
			break;
		}
	}

}
