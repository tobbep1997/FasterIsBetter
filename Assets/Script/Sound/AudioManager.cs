using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {



	public Slider VolumeSlider;
	public Toggle AudioToggle;



	void Update()
	{
		AudioListener.volume = VolumeSlider.value;
		if (AudioToggle.isOn) {
			AudioListener.volume = VolumeSlider.value;
		}
		else
			AudioListener.volume = 0;
	}
}
