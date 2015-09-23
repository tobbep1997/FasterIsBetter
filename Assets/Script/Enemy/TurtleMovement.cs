using UnityEngine;
using System.Collections;

public class TurtleMovement : MonoBehaviour {
    /// <summary>
    /// This makes sure that the turtle can move and makes sure that the player losses time when touching the turtle
    /// </summary>
	public Transform turnCheckStart, turnCheckMid, turnCheckEnd; //This are the positions for the rays that check if the enemy is moving in to a wall or off a platform
	public Transform groundCheck;

	public float moveSpeed;
	private bool moveRight = true;
	private bool turnPointGround = false;       //this is true if there is no ground for the turtle to procede on
	private bool turnPointWall = false;         //this is true if the turtle walks in to a wall
	private bool turnPointWood = false;         //this is true if the turtle has wood... infront of him
	private bool facingRight = false;
	private bool onGround = false, onWood = false;

	public float TimeScaleToRemove = 8;			//This is the time that is removed from the players timescale when is touched
	public float TimeToRemove = 4;
	public float invinsibliety = 2;             //This is the time before the character can take damage from this turtle again
	private float invisTimer;                   //this is a timer that starts after when the character touches the turtle and this is reseted when it reaches invinsibilityFloat
	public bool isInvinsible = false;

	private Rigidbody2D body2D;

	void Start () {
		body2D = GetComponent<Rigidbody2D> ();
		//moveSpeed = 1;
	}
	
	void Update () {
		Timer();
		//DRAW THESE FOR TESTING
		//Debug.DrawLine (turnCheckStart.position, turnCheckEnd.position, Color.red);
		//Debug.DrawLine (turnCheckStart.position, turnCheckMid.position, Color.green);

		//switches movement direction
        //Checks what way the turtle is looking and makes sure that its moving in the right direction
		if(moveRight)
			body2D.position += Vector2.right * moveSpeed * Time.deltaTime;
		else
			body2D.position += -Vector2.right * moveSpeed * Time.deltaTime;

		//checks for woodwall
		turnPointWood = Physics2D.Linecast (turnCheckStart.position, turnCheckMid.position, 1 << LayerMask.NameToLayer ("Wall"));
		//checks where wall is
		turnPointWall = Physics2D.Linecast (turnCheckStart.position, turnCheckMid.position, 1 << LayerMask.NameToLayer ("Ground"));
		//checks where ground ends
		turnPointGround = Physics2D.Linecast (turnCheckStart.position, turnCheckEnd.position, 1 << LayerMask.NameToLayer ("Ground"));

		onGround = Physics2D.Linecast(turnCheckStart.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		onWood = Physics2D.Linecast (turnCheckStart.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Wall"));
		if (!turnPointGround || turnPointWall || turnPointWood) {

			if(onGround || onWood)
			{
			moveRight = !moveRight;
			Flip ();
			}
		}
	}
    
	void Timer()
	{
        //This is a timer that is going after the player has touched the turtle
		if (isInvinsible == true) {
			invisTimer += TimeScale.DeltaTime;
			if (invisTimer >= invinsibliety) {
				isInvinsible = false;
				invisTimer = 0;
			}
		}
	}
	void OnCollisionStay2D(Collision2D other)
	{
        //checks if the turtle is touching the players
		if (isInvinsible == false)
		if (other.gameObject.tag == "Player") {
			TimeScale.RemoveTime(TimeScaleToRemove);
			UIBehavior.RemoveTime(TimeToRemove);
			isInvinsible = true;
		}
	}

	//flips object
	void Flip()
	{
		// Switch the way the player is labelled as facing
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
