using UnityEngine;
using System.Collections;
//Copyright (c) 2015 Tobias Pogén
/// <summary>
/// This makes sure that you can pick up all clocks and adds time to the players timescale
/// </summary>
public class ClockPickUp : MonoBehaviour {
	public float TimerToReduce = 5;
    public int score = 15;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			TimeScale.AddClocks(1);
            Score.AddScore(score);
			Destroy (gameObject);
		}
	}
}
