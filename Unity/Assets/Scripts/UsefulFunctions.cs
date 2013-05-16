using UnityEngine;
using System.Collections;

public static class UsefulFunctions : object {
	
	
	//
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------
	// Wrapper method for touch input target finding
	public static GameObject TapTarget (Touch touch, int layerMask = ~0)
	{
		Vector2 point = touch.position;
		return TapTarget(point, layerMask);
	}
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------
	//
	
	//
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------
	// Finds the clicked or tapped object at screen point point. Layer mask default to all.
	public static GameObject TapTarget (Vector2 point, int layerMask = ~0)
	{
	
		Ray ray = Camera.main.ScreenPointToRay (point);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100, layerMask)) {	
			return hit.collider.gameObject;	
		} else
			return null;
	}
	
	//
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------
	// Orients object self to look at other
	public static void objLookAt (GameObject self, GameObject other) {
		self.transform.LookAt(other.transform.position);
	}
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------
	//
	
	//
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------
	// Orients object self to look at position other
	public static void objLookAt (GameObject self, Vector3 other) {
		self.transform.LookAt(other);	
	}
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------
	//
	
	//
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------
	// Converts a Rect used for GUI into an equivilent rect used for iPhone input.
	public static Rect GUItoInput (Rect rectToConvert)
	{
		Rect convertedRect = new Rect (rectToConvert.x, Screen.height - rectToConvert.y - rectToConvert.height, rectToConvert.width, rectToConvert.height);
		return convertedRect;
	}
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------
	//

	

	//Skeleton for double tap on an array of objects
//	void dt () {
//		GameObject[] objects;
//		GameObject tappedObject; // Would get this via tappedtarget method
//		int tapIndex = 0;
//		
//		
//		switch(tapIndex) {
//		case 0:
//			for(int i = 0; i < objects.Length; i++) {
//				if(tappedObject == objects[i]) tapIndex = i;	
//			}
//				
//				break;
//		default:
//			break;
//		}
//			
//	}



}
