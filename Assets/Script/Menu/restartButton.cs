using UnityEngine;
using System.Collections;

public class restartButton : MonoBehaviour {


	public void Restart ()
	{
		TimeScale.ResetValues(true);
		Application.LoadLevel(Application.loadedLevel);
	}
}
