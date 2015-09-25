using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {
	
	public WoodWall woodWall;

	public GameObject clickOne, clickTwo, clickThree, clickFour;
	public bool firstClicked = false, secondClicked = false, thirdClicked = false;
	void Start () {
		clickTwo.SetActive (false);
		clickThree.SetActive (false);
		clickFour.SetActive (false);
	
	}

	void Update () {
		TutorialStateChecker();
	
		if (firstClicked && !secondClicked) {
			Destroy (clickOne);
			clickTwo.SetActive(true);
		}

		if (secondClicked && !thirdClicked) {
			Destroy(clickTwo);
			clickThree.SetActive(true);
		}

		if (thirdClicked) {
			Destroy(clickThree);
		}

		if (woodWall.inRange) {
			clickFour.SetActive(true);
			if(woodWall.wallLowered){
				Destroy(clickFour);
			}
		}

	}
	void TutorialStateChecker()
	{
		if (Input.touchCount > 0) {
			//bool resetDir = true;
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch(i).position.x >= (Screen.width/3)*2 && Input.GetTouch(i).position.y < (Screen.height/3)*2) {
					firstClicked = true;
				}
				else if (Input.GetTouch(i).position.x <= (Screen.width/3) && Input.GetTouch(i).position.y < (Screen.height/3)*2)
				{
					if(firstClicked)
						secondClicked = true;
				}
				
				
				if (Input.GetTouch(i).position.x > (Screen.width/3) && 
				    Input.GetTouch(i).position.x < (Screen.width/3)*2 && 
				    Input.GetTouch(i).position.y < (Screen.height/3))
				{
					if(secondClicked)
						thirdClicked = true;
				}
				if (Input.GetTouch(i).position.x > (Screen.width/3) && 
				    Input.GetTouch(i).position.x < (Screen.width/3)*2 && 
				    Input.GetTouch(i).position.y > (Screen.height/3))
				{
					//DO SAME STUFF AS THE CLICK BOOLS
					
				}
			}
		}
	}
}
