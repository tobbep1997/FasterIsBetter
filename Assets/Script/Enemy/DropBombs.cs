using UnityEngine;
using System.Collections;

public class DropBombs : MonoBehaviour {
	public Transform DropPosition;
	public GameObject DropObject;
	public GameObject ReloadBulletDrop;

	public float TimeBetweenDrops = 15;
	public float TimeBeforeFirstDrop = 5;

	private bool startDroppingBombs = false;
	private GameObject ReloadBullet;
	private float timer, startTimer;

	void Start()
	{
		timer = TimeBetweenDrops;
	}

	void Update()
	{
		if (startDroppingBombs == false) {
			startTimer += TimeScale.DeltaTime;
			if (startTimer >= TimeBeforeFirstDrop) {
				startDroppingBombs = true;
			}
		}
		if (startDroppingBombs) {
			timer += TimeScale.DeltaTime;
			if (timer >= TimeBetweenDrops) {
				dropBombs();
				timer = 0;
			}
		}
	}
	void dropBombs()
	{
		bool dropShell = false;

		if (ReloadBullet == null) {
			if (Random.value > .5f) {
				dropShell = true;
			}
		}

		if (dropShell) {
			ReloadBullet = (GameObject)Instantiate (ReloadBulletDrop, DropPosition.position, Quaternion.identity);

		} else
			Instantiate (DropObject, DropPosition.position, Quaternion.identity);
	}


}
