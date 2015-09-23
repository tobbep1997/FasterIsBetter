using UnityEngine;
using System.Collections;

public class CharacterJumpController : MonoBehaviour {
    /// <summary>
    /// This checks if the player can jump and if the player is touching ground
    /// </summary>
	public CharacterController charController; //This stores the main character controller

	void Start () {
		charController = GetComponentInParent<CharacterController> ();
	}
    
	void OnCollisionStay2D(Collision2D col)//this is sent a update tick every time its coliding with something
	{	
		//This checks if the player stands on ground
		if (col.gameObject.tag == "Ground")	{
			charController.canJump = true;
			charController.IsGrounded = true;
		}   else {
			charController.canJump = false;	
			charController.IsGrounded = false;
		}


	}
    //This is called once when the collider stop colliding with something
	void OnCollisionExit2D(Collision2D col)
	{
		//checks if they player leaves ground
		if (col.gameObject.tag == "Ground")
		{
			charController.canJump = false;	
			charController.IsGrounded = false;
		}
	}
}
