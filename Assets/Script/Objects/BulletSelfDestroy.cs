using UnityEngine;
using System.Collections;

public class BulletSelfDestroy : MonoBehaviour {


	public float TimeToDestroy = 1;
	private float Timer;
	
	// Update is called once per frame
	void Update () {
		Timer += TimeScale.DeltaTime;
		if (Timer > TimeToDestroy) {
			EventManager.CallPlayerAttack();
			Destroy(gameObject);
		}
	}
}
