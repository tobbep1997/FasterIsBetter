using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Exploation : MonoBehaviour {

	public float ExplotionRadious;
	public float ExplotionTime;

	public float ExploationForce;

	public  CircleCollider2D colider;

	private float timer;
	private float multiplier;
	private float currentRadious;
	private float startRadious;
	
	private bool explode = false;

	void Start () {
		startRadious = colider.radius;
		currentRadious = startRadious;
		multiplier = (ExplotionRadious - startRadious) / ExplotionTime;
	}

	void Update () {
		if (explode) {
			timer += Time.deltaTime;
			currentRadious = multiplier * timer;
		}
		if (timer >= ExplotionTime) {
			Destroy(gameObject);
		}
		ApplyExploationForce(currentRadious,ExploationForce);
		colider.radius = currentRadious;
	}

	void ApplyExploationForce(float Radious,float force)
	{
		Collider2D[] other = Physics2D.OverlapCircleAll(transform.position,Radious);
		List<Collider2D> objectsInOther = new List<Collider2D>(); 

		foreach (Collider2D item in other) {
			if (item.tag == "Player" || item.tag == "Shell" && objectsInOther.AnyInstanceIsEqual(item) != true)
				objectsInOther.Add(item);
			if (item.attachedRigidbody != null)
				if ((Vector2.one * force).IsBigger(item.attachedRigidbody.velocity))
					item.attachedRigidbody.AddForce((item.transform.position-transform.position).normalized * force);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		explode = true;
	}
}
