using UnityEngine;
using System.Collections;

public class restartButton : MonoBehaviour {


	public void Restart ()
	{
		TimeScale.ResetValues(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
}
