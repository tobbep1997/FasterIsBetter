using UnityEngine;
using System.Collections;

public class GrassScript : MonoBehaviour {
	/// <summary>
	/// Grass behavior
	/// </summary>

	
	Animator anim;	
	public Transform particle;              //this is the particles
	
	private Vector2 playerPrevPos;          //This is the previous pos of the player
	public float distanceOffset = 0.01f;	//This is the distance the player has to move for the grass to spawn any particles
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		playerPrevPos = transform.position;
		playerPrevPos = Vector2.zero;
	}
	
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") {
			PlayerIntersect(true);
			if (Vector2.Distance(other.transform.position,playerPrevPos) > distanceOffset) {
				Instantiate(particle,other.transform.position,Quaternion.identity);
				playerPrevPos = other.transform.position;
			}
			else
				playerPrevPos = other.transform.position;
		}
		else
			PlayerIntersect(false);
		
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player" || other.tag == "Enemy")
		{
			PlayerIntersect(false);
		}
	}
	void PlayerIntersect(bool b)
	{
		anim.SetBool("PlayerIntersect",b);
	}
}
