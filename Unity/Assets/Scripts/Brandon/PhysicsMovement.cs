using UnityEngine;
using System.Collections;

public class PhysicsMovement : MonoBehaviour {
	
	public Rigidbody character;
	public float movementForce;
	public float jumpForce;
	public float jumpLength;
	bool jump;
	
	void Start () {
	
	}
	
	
	void FixedUpdate () {
		if (Input.GetMouseButton(0)) {
			//add force to move character
			character.AddRelativeForce (Vector3.forward*movementForce);
			//find where the mouse is clicking in the level, and look at it
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rayHit;
       		if (Physics.Raycast(ray,out rayHit, 100)) {
				character.transform.LookAt(new Vector3 (rayHit.point.x, character.position.y, rayHit.point.z));       		
			}
			
		}
		//print (character.velocity.y);
		
	}
	
	void OnCollisionEnter (Collision collider) {
		//jump when you run into a wall
		if (character.velocity.y > -0.000001f && !jump) {
			jump = true;
			//to prevent double jumps
			Invoke("JumpDelay", jumpLength);
			character.AddRelativeForce (Vector3.up*jumpForce);
		}
	}
	void JumpDelay () {
		jump = false;
	}
}
