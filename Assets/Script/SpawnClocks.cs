using UnityEngine;
using System.Collections;

public class SpawnClocks : MonoBehaviour {

	public GameObject Clock;
	public Transform SpawnPos;

	public float TimeBetweenSpawn = 24;

	public bool StartSpawnClock = true;

	private float timer;
	private GameObject SpawnedClock;
	void Start()
	{
		if (StartSpawnClock) {
			SpawnClock();
		}
	}
	void Update()
	{
		if (SpawnedClock == null)
			timer += TimeScale.DeltaTime;
		if (timer >= TimeBetweenSpawn) {
			SpawnClock();
			timer = 0;
		}
	}

	void SpawnClock()
	{
		SpawnedClock = (GameObject)Instantiate (Clock, SpawnPos.position, Quaternion.identity);
	}
}
