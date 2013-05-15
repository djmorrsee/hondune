using UnityEngine;
using System.Collections;

public class PhysicsMovement : MonoBehaviour {
	
	public Rigidbody character;
	public float movementForce;
	public float jumpForce;
	public float jumpLength;
	public float armLength;
	public float characterHeight;
	public float additionalGravity;
	public float rotationSpeed;
	bool jumping;
	bool canJump;
	Vector3 wantedPos;
	Vector3 currentPos;
	void Start () {
	
	}
	
	
	void FixedUpdate () {
		//add additional gravity to make up for the movement drag
		character.AddForce (Vector3.down*additionalGravity);
		
		if (Input.GetMouseButton(0)) {
			//add force to move character
			character.AddRelativeForce (Vector3.forward*movementForce);
			//find where the mouse is clicking in the level, and save that position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rayHit;
       		if (Physics.Raycast(ray,out rayHit, 100)) {
				currentPos = new Vector3 (rayHit.point.x, character.position.y, rayHit.point.z);     		
			}
		}
		//lerp to the wanted look at position, to make rotations smooth
		wantedPos = Vector3.Lerp (wantedPos, currentPos, Time.deltaTime*rotationSpeed);
		//make sure height is always the same as the characters, to avoid looking up or down
		wantedPos = new Vector3 (wantedPos.x, character.position.y, wantedPos.z);
		//look at the wanted position
		character.transform.LookAt(wantedPos);  
		
		//if you are close enough to a wall, allow jumping
		Vector3 forward = character.transform.TransformDirection(Vector3.forward);
		RaycastHit rayHit2;
        if (Physics.Raycast(character.transform.position, forward,out rayHit2, armLength)) {
			canJump = true;
		}
		else {
			canJump = false;
		}
		
		//if character is not touching the ground, jumping is true
		Vector3 down = character.transform.TransformDirection(Vector3.down);
		RaycastHit rayHit1;
        if (Physics.Raycast(character.transform.position, down,out rayHit1, characterHeight)) {
			jumping = false;
		}
		else {
			jumping = true;
		}
		
	}
	
	void OnCollisionEnter (Collision collider) {
		//jump when you run into a wall
		if (canJump && !jumping) {
			character.AddRelativeForce (Vector3.up*jumpForce);
			jumping = true;
		}
	}
}
