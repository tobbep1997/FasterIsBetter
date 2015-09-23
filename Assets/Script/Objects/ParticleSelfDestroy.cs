using UnityEngine;
using System.Collections;
/// <summary>
/// This makes a object self destroy after an amount of time
/// </summary>
public class ParticleSelfDestroy : MonoBehaviour {

	public float TimeToDestroy = 1;
	private float Timer;
	
	// Update is called once per frame
	void Update () {
		Timer += TimeScale.DeltaTime;
		if (Timer > TimeToDestroy) {
			Destroy(gameObject);
		}
	}
}
