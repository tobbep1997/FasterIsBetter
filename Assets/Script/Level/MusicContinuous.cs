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
		if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
			Destroy (gameObject);
	}
}
