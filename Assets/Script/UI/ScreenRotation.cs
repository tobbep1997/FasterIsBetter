using UnityEngine;
using System.Collections;

public class ScreenRotation : MonoBehaviour {
	void Start () {
		Screen.orientation = ScreenOrientation.Landscape;
	}
}
