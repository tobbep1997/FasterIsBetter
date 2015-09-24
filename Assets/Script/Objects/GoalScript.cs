using UnityEngine;
using System.Collections;
/// <summary>
/// This is the called when the player interact with the goal clock
/// </summary>
public class GoalScript : MonoBehaviour {

	int CurrentLevel;//Gets the currentLevel
	bool Wonned = false;//is true if the player won

	float timerBeforeEnd = 1.5f;//time between getting to the goal and loadning the next level
	float timer;

	public Transform particle;
	public bool LoadMenu = false;//loads to menu if checked



	public float TimerBeforeEnd
	{
		get
		{
			return timerBeforeEnd;
		}
		set
		{
			if (value >= .5f && value <= 2) {
				timerBeforeEnd = value;
			}
			else {
				if (value < .5f) {
					timerBeforeEnd = .5f;
				}
				if (value > 2) {
					timerBeforeEnd = 2;
				}
			}
		}
	}

	void Start()
	{
		CurrentLevel = Application.loadedLevel;
	}
	void OnTriggerEnter2D(Collider2D other)//check if the object trigger interact with the player
	{
		if (other.tag == "Player" && !Wonned) {
			if (LoadMenu) {
				Application.LoadLevel("Main_menu");
			}
			else
			{
				TimeScale.ResetValues(true);
				Instantiate(particle,transform.position,Quaternion.identity);
				TimeScale.timeTicking = false;
                UIBehavior.Vicory();
                Score.FinishLevel(UIBehavior.fCurrentTime);
				Wonned = true;
			}
            try
            {
                other.GetComponent<CharacterController>().enabled = false;
            }
            catch (System.Exception)
            {
                other.transform.parent.GetComponent<CharacterController>().enabled = false;
                throw;
            }
            if (other.GetComponent<CharacterController>() == null)
            {
                other.transform.parent.GetComponent<CharacterController>().enabled = false;
            }
		}
	}   
}
