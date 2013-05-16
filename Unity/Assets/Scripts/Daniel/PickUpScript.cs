using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour
{

	bool tap = false;
	float tapStartTime = 0.0f;
	const float tapDelayDuration = 0.75f;
	bool carrying = false;
	
	void Update ()
	{
		if (carrying) {
			targetObject.transform.position = gameObject.transform.position;
			targetObject.transform.position += Vector3.up * 1.5f;
			DoSetDown ();
		} else {
			DoPickup ();
		}
	}
	
	GameObject targetObject;
	const float pickUpDistance = 1.5f;

	void DoPickup ()
	{
		if (!tap) {
			if (Input.GetMouseButtonDown (0)) {
				targetObject = UsefulFunctions.TapTarget (Input.mousePosition);
				if (targetObject == null)
					return;
				
				CanPickUp canPickUp = targetObject.GetComponent<CanPickUp> ();
				if (canPickUp == null) {
					return;
				} else {
					// Will check the canPickUp Boolean later
					tapStartTime = Time.timeSinceLevelLoad;
					tap = true;	
				}
			}
			
		} else {
			if (Time.timeSinceLevelLoad - tapStartTime > tapDelayDuration) {
				tap = false;	
			} else if (Input.GetMouseButtonDown (0)) {
				if (targetObject == UsefulFunctions.TapTarget (Input.mousePosition)) {
					if ((gameObject.transform.position - targetObject.transform.position).sqrMagnitude < pickUpDistance * pickUpDistance) {
						carrying = true;
						tap = false;
					} else {
						// To far away
						// Could implement a walk to here?
					}
					
				} else {
					tap = false;
				}
			}
		}
	}

	GameObject collisionTile;
	string name;

	void DoSetDown ()
	{
		int layerMask = 1 << 9; // Whatever layer the CollisionTile is
		if (!tap) {
			if (Input.GetMouseButtonDown (0)) {
				collisionTile = UsefulFunctions.TapTarget (Input.mousePosition, layerMask);
				if (collisionTile == null)
					return;
				
				tapStartTime = Time.timeSinceLevelLoad;
				tap = true;
			}
			
		} else {
			if (Time.timeSinceLevelLoad - tapStartTime > tapDelayDuration) {
				tap = false;	
			} else if (Input.GetMouseButtonDown (0)) {
				
				if (collisionTile == UsefulFunctions.TapTarget (Input.mousePosition)) {
					if ((gameObject.transform.position - collisionTile.transform.position).sqrMagnitude < pickUpDistance * pickUpDistance + .5f) {
						targetObject.collider.enabled = false;
						targetObject.transform.position = new Vector3 (collisionTile.transform.position.x, Mathf.CeilToInt (collisionTile.transform.position.y), collisionTile.transform.position.z);
						carrying = false;
						
						
						CheckCollsions();
					} else {
						// To far away
						// Could implement a walk to here?
						print ("Too Far Away");
					}
					
				} else {
					tap = false;
				}
			}
		}
		
	}
	
	bool CheckCollsions ()
	{
		Vector3 scale = collisionTile.transform.localScale;
		collisionTile.transform.localScale = Vector3.Scale (collisionTile.transform.localScale, 5 * Vector3.Scale (Vector3.one, Vector3.up * 2));
		if (collisionTile.collider.bounds.Contains (gameObject.transform.position)) {
			print ("In Radius");
			
		} else {
			print ("Not in Radius");	
			targetObject.collider.enabled = true;
			tap = false;
		}
		collisionTile.transform.localScale = scale;
		return true;
	}
			
	static Vector3 MidPoint (Vector3 start, Vector3 end, float percent)
	{
		return new Vector3 ((start.x + end.x) * percent, (start.y + end.y) * percent, (start.z + end.z) * percent);

	}
			
}

