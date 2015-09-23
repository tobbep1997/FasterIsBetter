using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds; //list of all the fore- and backgrounds in the parallaxing
	private float[] parallaxScales;	//the proportion of the cameras movement to move the backgrounds by
	public float smoothing = 1f;	//set value above 0 or else it wont work

	private Transform cam;			//reference to the main cameras transform
	private Vector3 previousCamPos;	//pos of camera in previous frame

	//called before start, use for references for other objects
	void Awake(){
		cam = Camera.main.transform;
	}

	void Start () {
		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z * -1; //adds a seperate float value for each background
		}
	}

	void Update () {
	
		for (int i = 0; i < backgrounds.Length; i++) {
			//the parallax is the opposite of the camera movement, due to the previous frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			//set x position which equals current pos plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			//create target position for the bg's current position with its target x position
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			//fade between current pos and target pos using lerp
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		//set prev cam pos to cam's pos at end of frame
		previousCamPos = cam.position;
	}
}
