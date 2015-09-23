using UnityEngine;
using System.Collections;
/// <summary>
/// This is a cannon that can fire bullets on the players command
/// </summary>
public class Cannon : MonoBehaviour {

	public bool cannonLoaded = false;   

	public GameObject Bullet;           //the object the cannon will fire

	public Transform MuzzlePosition;    //this is where the bullet will spawn

	public float BulletSpeed = 50;     

	public bool CanInteract;

	void Start()
	{
		EventManager.OnPlayerInteract += HandleOnPlayerInteract;
	}

	void HandleOnPlayerInteract ()
	{
		if (CanInteract)
		if (cannonLoaded)
			fireCannon();
		else
			cantFire();
	}

	void OnTriggerStay2D(Collider2D other)
	{
        //In here i check if the player is in range and if so makes sure that the player can fire by interacting with the keypress
        //it also checks if a new Shell is in range and loads the cannon if its in range
		if (other.tag == "Player") {
			CanInteract = true;
		}
		else
			CanInteract = false;

		if (other.tag == "Shell") {
			if (isCannonLoaded())
				Destroy(other.gameObject);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") {
			CanInteract = false;
		}
	}


	void fireCannon()
	{

        //This fires the cannon
		GameObject temp = (GameObject)Instantiate(Bullet,MuzzlePosition.position,Quaternion.identity);
		Rigidbody2D tempBody = temp.GetComponent<Rigidbody2D>();
		tempBody.AddForce(Vector2.up*BulletSpeed);

		cannonLoaded = false;

	}
	void cantFire()
	{
		print("You cant shoot you stoopid baka");
	}
	bool isCannonLoaded()
	{
        //return true if the cannon has been loaded and return false if the cannon is loaded
		if (cannonLoaded != true) {
			cannonLoaded = true;
			return true;
		}
		else
			return false;
	}
}
