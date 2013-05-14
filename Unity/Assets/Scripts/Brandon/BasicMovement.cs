using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {

	public int rayLength;
	public GameObject character;
	public float characterHeight;
	public float armLength;
	public float movementSpeed;
	public float jumpSpeed;
	public float fallSpeed;
	public float jumpHeight;
	Vector3 wantedPos;
	bool jumping;
	
	void FixedUpdate () {
		//fall untill you hit something
		Vector3 down = character.transform.TransformDirection(Vector3.down);
		RaycastHit rayHit1;
        if (Physics.Raycast(character.transform.position, down,out rayHit1, characterHeight)) {
			
		}
		else if (!jumping) {
			character.transform.position = new Vector3 (character.transform.position.x, character.transform.position.y-fallSpeed, character.transform.position.z);
		}
		
		//check for stuff in front of you and attempt to jump over it
		Vector3 forward = character.transform.TransformDirection(Vector3.forward);
		RaycastHit rayHit2;
        if (Physics.Raycast(character.transform.position, forward,out rayHit2, armLength) && !jumping) {
			jumping = true;
			Invoke ("disableJump", jumpSpeed);
			iTween.MoveAdd(character, iTween.Hash("y", character.transform.position.y+jumpHeight, "easeType", "EaseOutSine","time", jumpSpeed));
		}
		else {
			
		}
		
		//click anywhere on the level to move to there
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rayHit3;
       		if (Physics.Raycast(ray,out rayHit3, rayLength)) {
				//print ("ray hit");
				wantedPos = rayHit3.point;
           		iTween.MoveTo(character, iTween.Hash("x", rayHit3.point.x, "z", rayHit3.point.z, "easeType", "linear","time", movementSpeed));
			}
		}
	}
	void disableJump () {
		jumping = false;
	}
}
