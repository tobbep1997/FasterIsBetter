using UnityEngine;
using System.Collections;

public class spinObject : MonoBehaviour {

	public float speed = 5;
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z + speed);
	}
}
