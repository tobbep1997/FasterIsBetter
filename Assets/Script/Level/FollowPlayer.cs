using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    /// <summary>
    /// This follows the player on the X-axis
    /// </summary>
	public Transform Player;
	void Start()
	{
		//Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		Player = Camera.main.gameObject.transform;
	}

	void Update()
	{
		transform.position = new Vector3(Player.position.x,transform.position.y,transform.position.z);
	}
}
