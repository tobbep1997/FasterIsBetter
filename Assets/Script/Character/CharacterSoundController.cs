using UnityEngine;
using System.Collections;

public class CharacterSoundController : MonoBehaviour {
	public AudioSource clockSFX;
	public AudioSource brushSFX;
	public AudioSource goalSFX;
	public AudioSource speedSFX;

	public AudioSource jump1, jump2, jump3;

	void Start () {
	
	}

	public void JumpSound()
	{
		int temp;
		temp = Random.Range (0, 2);

		switch (temp) {
		case 0:
				jump1.Play();
			break;
		case 1:
				jump2.Play ();
			break;
		case 2:
				jump3.Play();
			break;
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "Clock")
			clockSFX.Play ();

		if (other.transform.tag == "TallGrass")
			brushSFX.Play ();

		if (other.transform.tag == "Goal")
			goalSFX.Play ();

		if (other.transform.tag == "SpeedBoost")
			speedSFX.Play ();
	}
}
