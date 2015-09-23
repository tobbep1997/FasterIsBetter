using UnityEngine;
using System.Collections;

public class LogoFade : MonoBehaviour {

	public float logoFadeInDelay = 3.0f;
	public float logoFadeInTimer = 3.0f;

	private float introTimer;
	public float introTimerLimit = 4;

	SpriteRenderer spriteRenderer;

	void Start () {

		spriteRenderer = GetComponent<SpriteRenderer> ();
	
	}
	void Update () {
		logoFadeInTimer -= Time.deltaTime;
		introTimer += Time.deltaTime;
		spriteRenderer.color = new Color(1, 1, 1, 1 - logoFadeInTimer / logoFadeInDelay);

		if (introTimer > introTimerLimit)
			Application.LoadLevel ("Main_Menu");
	
	}
}
