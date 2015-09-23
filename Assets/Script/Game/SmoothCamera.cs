﻿using UnityEngine;
using System.Collections;
	
public class SmoothCamera : MonoBehaviour {
	
	public float dampTime = 0.15f;

	private Vector3 velocity = Vector3.zero;
	public Transform target;

	Camera cam;

	void Start()
	{
		cam = gameObject.GetComponent<Camera>();

		//target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	

	void Update () 
	{	
		if (target)
		{
			Vector3 point = cam.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		
	}
}

