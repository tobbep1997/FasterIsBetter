using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

	public GameObject bulletHit;
	public Transform bulletSpawn,bulletHitExplotion;
	public GameObject FollowObject;
	public float MovementSpeed;

	public Transform GoalPhysicSpawn;
	public GameObject GoalClock;

	public int Health = 10;

	// Use this for initialization
	void Start () 
	{
		EventManager.OnPlayerAttack += AttackShip;
		FollowObject = GameObject.FindGameObjectWithTag("Player");
	}
	void OnDestroy()
	{
		EventManager.OnPlayerAttack -= AttackShip;
	}
	

	void Update () {
		FollowPlayer();
		CheckIfDestroyed ();
	}

	void CheckIfDestroyed()
	{
		if (Health <= 0) {
			DestroyShip();
		}
	}
	void DestroyShip()
	{
		Instantiate (GoalClock, GoalPhysicSpawn.position, Quaternion.identity);
		Destroy (gameObject);
	}

	void AttackShip()
	{
		//GameObject temp = (GameObject)Instantiate(bulletHit,bulletSpawn.position,Quaternion.identity);
		DamageShip ();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bullet") {
			Destroy(other.gameObject);
		}
	}
	void DamageShip()
	{
		Health -= 1;
	}
	void FollowPlayer()
	{
		Vector3 temp = (FollowObject.transform.position - transform.position).normalized *
					   (MovementSpeed * Vector3.Distance(transform.position,FollowObject.transform.position)) * TimeScale.DeltaTime;
		transform.Translate(new Vector2(temp.x,0),Space.World);
	}
}
