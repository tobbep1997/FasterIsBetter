using UnityEngine;
using System.Collections;

public class MusicContinuous : MonoBehaviour {
	void Awake()
	{
		DontDestroyOnLoad (transform.gameObject);
	}

	void Update()
	{
		destroyInMenu ();
	}

	public void destroyInMenu()
	{
		if(Application.loadedLevel == 1)
			Destroy (gameObject);
	}
}
