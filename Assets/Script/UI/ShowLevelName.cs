using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowLevelName : MonoBehaviour {

	private RectTransform rectTransform;

	public string levelText;
	public float fadeTime = 5;
	private float startTime, prosAlpha;

	public Text displayText;

	public Color ChangeToColor;
	private Color StartColor;

	void Start () {
		startTime = fadeTime;
		StartColor = displayText.color;
		rectTransform = GetComponent<RectTransform> ();

	}

	void Update () {
		prosAlpha = 1*(fadeTime/startTime);
		fadeTime -= TimeScale.DeltaTime;
		ChangeToColor.a = prosAlpha;

		PanMovement ();
	}
	void OnGUI()
	{
		displayText.text = levelText;	
		displayText.material.color = ChangeToColor;
	}

	void PanMovement()
	{
		rectTransform.position += new Vector3 (13, 0, 0) * Time.deltaTime;
	}
}
