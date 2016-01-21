using UnityEngine;
using System.Collections;

public class DeveloperStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.N)) {
			TimeScale.ResetValues(true);
			//Instantiate(particle,transform.position,Quaternion.identity);
			TimeScale.timeTicking = false;
			//Wonned = true;
			//.LoadLevel(Application.loadedLevel + 1);
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
			TimeScale.timeTicking = true;
		}
	}
}
