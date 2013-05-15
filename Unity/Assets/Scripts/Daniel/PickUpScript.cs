using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {

	bool tap = false;
	float tapStartTime = 0.0f;
	const float tapDelayDuration = 0.75f;

	bool carrying = false;
	
	void Update () {
		if(carrying) {
			targetObject.transform.position = gameObject.transform.position;
			targetObject.transform.position += Vector3.up * 1.5f;
		} else {
			DoPickup();
		}
	}
	
	GameObject targetObject;
	const float pickUpDistance = 1.5f;
	void DoPickup () {
		if(!tap) {
			if(Input.GetMouseButtonDown(0)) {
				targetObject = TapTarget(Input.mousePosition);
				print (targetObject.name);
				CanPickUp canPickUp = targetObject.GetComponent<CanPickUp>();
				if(canPickUp == null) {
					return;
				} else {
					tapStartTime = Time.timeSinceLevelLoad;
					tap = true;	
				}
			}
			
		} else {
			if(Time.timeSinceLevelLoad - tapStartTime > tapDelayDuration) {
				tap = false;	
			} else if (Input.GetMouseButtonDown(0)) {
				if(targetObject == TapTarget(Input.mousePosition)) {
					if((gameObject.transform.position - targetObject.transform.position).sqrMagnitude < pickUpDistance * pickUpDistance) {
						carrying = true;
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
	
	public static GameObject TapTarget (Touch touch)
	{
		Vector2 point = touch.position;
		return TapTarget(point);
	}
	
	public static GameObject TapTarget (Vector2 point)
	{
		Ray ray = Camera.main.ScreenPointToRay (point);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {	
			return hit.collider.gameObject;	
		} else
			return null;
	}
}

