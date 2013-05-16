using UnityEngine;
using System.Collections;

public class GenerateCollisionTiles : MonoBehaviour
{
	
	public int 		width;
	public int		height;
	GameObject 		collisionTile;
	Vector3 		pos;
	
	// Use this for initialization
	void Start ()
	{
		collisionTile = Resources.Load ("Daniel/CollisionTile") as GameObject;
		if (collisionTile == null)
			print (null);
		
		pos = gameObject.transform.position;
		
		AdjustPos ();
		CreateGrid ();
		//Destroy (this);
		
		
	}
	
	void AdjustPos ()
	{
		if (width % 2 == 0)
			pos += Vector3.forward * 0.5f;
		else 
			pos -= Vector3.forward * 0.5f;
		if (height % 2 == 0)
			pos += Vector3.right * 0.5f;
		else 
			pos -= Vector3.right * 0.5f;
		
		pos += Vector3.up * renderer.bounds.size.y / 2;
		
		pos -= Vector3.forward * width / 2;
		pos -= Vector3.right * height / 2;
	}
	
	void CreateGrid ()
	{
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject t = (GameObject)Instantiate (collisionTile, pos + Vector3.right * i + Vector3.forward * j, Quaternion.identity);
				t.transform.parent = gameObject.transform;
				t.layer = 9;
				t.name = ((i+1) + (j) * width).ToString();
				//pos + Vector3.right * i + Vector3.forward * j
			}
		}
	}
}



