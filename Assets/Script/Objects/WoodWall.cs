using UnityEngine;
using System.Collections;
/// <summary>
/// Woodwall is a in game object that the player can interact with and is lowerd when interacted with
/// </summary>
public class WoodWall : MonoBehaviour {

	public Transform particle;

	public Transform clickCheckStart, clickCheckEnd;
	public Transform loweredCheckStart, loweredCheckEnd;

	public Transform firstPos;

	public bool inRange = false;
	private bool touchGround = false;
	public bool wallLowered = false;

	public int lowerSpeed = 1;

	//public Transform ParticleSpawn;

	private float TimeBetweenCalls = .5f;
	private float timer;
	
	void Start () {
		//this declares how high the wall was when it was placed, so that it doesn't raise infinitely high when pressing Z
		//firstPos = transform;
		//instead of using this i put firstPos in hierchy to 0,0,0
		EventManager.OnPlayerInteract += HandleOnPlayerInteract;

	}

	void HandleOnPlayerInteract ()
	{
		if (inRange && timer >= TimeBetweenCalls) {
			wallLowered = !wallLowered;
			timer = 0;
		}	
	}
	void Update () {

		timer += Time.deltaTime;
		

		//inrange controls when the player is close enough to interact with the wall, touchground checks when the woodwall hits the floor
		inRange = Physics2D.Linecast (clickCheckStart.position , clickCheckEnd.position, 1 << LayerMask.NameToLayer ("Player"));
		touchGround = Physics2D.Linecast (loweredCheckStart.position , loweredCheckEnd.position, 1 << LayerMask.NameToLayer ("Ground"));

		Debug.DrawLine (clickCheckStart.position, clickCheckEnd.position);

		//the switcher between lowering and raising the wood wall
//		if (inRange && Input.GetKeyDown (KeyCode.Z))
//			wallLowered = !wallLowered;

		//moves wall down if its going up or is at max up and the player clicks Z
		if (wallLowered && !touchGround) {
			//Instantiate(particle,ParticleSpawn.position,Quaternion.identity);
			//keeps the interaction field at the same height
			clickCheckStart.position += Vector3.up *lowerSpeed * Time.deltaTime;
			clickCheckEnd.position += Vector3.up *lowerSpeed * Time.deltaTime;
			//keeps the max height at one spot
			firstPos.position += Vector3.up *lowerSpeed * Time.deltaTime;
			firstPos.position += Vector3.up *lowerSpeed * Time.deltaTime;
			//lowers wall
			transform.position += -Vector3.up * lowerSpeed * Time.deltaTime;
		}

		//moves wall up if its going down or is at max down and the player clicks Z
		if (!wallLowered && transform.position.y < firstPos.position.y ) {
			//keeps the interaction field at the same height
			clickCheckStart.position += -Vector3.up *lowerSpeed * Time.deltaTime;
			clickCheckEnd.position += -Vector3.up *lowerSpeed * Time.deltaTime;
			//keeps the max height at one spot
			firstPos.position += -Vector3.up *lowerSpeed * Time.deltaTime;
			firstPos.position += -Vector3.up *lowerSpeed * Time.deltaTime;
			//raises wall
			transform.position += Vector3.up * lowerSpeed * Time.deltaTime;
		}
	}
}

