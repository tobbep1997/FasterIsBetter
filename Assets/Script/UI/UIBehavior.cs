using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
/// <summary>
/// This makes sure that all the ui elements updates
/// </summary>
public class UIBehavior : MonoBehaviour {

	public Text TimerText;              //The timer text
	public Slider TimeScaleSlider;      //The time scale indicator
	public static float fCurrentTime;          //The currentTime in a float format
	public float fStartTime;            //The start time that the timer is counting down from

	public AudioSource ClockSound;      //A sound that plays at the start of every second

	private static bool bDead = false;  //player dead
	public Canvas cDead;                //a canvas that is showd when the player is dead
	private static bool bVicory = false;//a canvas that is showd when the player has won

	int iPreviousTime;                  //the time the previous game tick

	void Start()
	{
		fCurrentTime = fStartTime;
	}
	void Update()
	{
        //if the player is playing update this script
		if (TimeScale.playing && TimeScale.timeTicking) {
			Timer();
			TimeSpeedIndicator();
			PlayClockSound(fCurrentTime);
		}
	}
	public static void Dead(bool value)
	{
		bDead = value;
	}
	public static void Vicory()
	{
		bVicory = true;
	}
    //This runs trough the restart protocols
	public static void Restart()
	{
		bDead = false;
		bVicory = false;
		TimeScale.ResetValues(true);
		TimeScale.StopTime(false);
		TimeScale.playing = true;
		Application.LoadLevel(Application.loadedLevel);
	}
    //this is the player wants to load the main menu
	public void LoadMainMenu()
	{
		Application.LoadLevel("Main_Menu");
	}


    //this plays a clock sound every second
	void PlayClockSound(float currentTime)
	{
		int iCurrentTime = (int)currentTime;
		if (iCurrentTime != iPreviousTime) {
			ClockSound.Play();
		}
		iPreviousTime = iCurrentTime;


	}
    //This updates the timescale indicator
	void TimeSpeedIndicator()
	{
		TimeScaleSlider.value = TimeScaleToIndicator();
	}
    //this returns a % values of the timescale
	float TimeScaleToIndicator()
	{	
		return 1 - ((TimeScale.fTimeScale - 1)/(TimeScale.TimeToEnd - 1));	
	}

	public static void RemoveTime(float time)
	{
		fCurrentTime -= time;
	}
    //updates timer
	void Timer()
	{
		OutOfTime(fCurrentTime);
		if (fCurrentTime > 0) {		
			fCurrentTime = TimeToRemove(fCurrentTime);
			TimerText.text = ConvertToClockString(CheckMinutes((int)fCurrentTime),CheckSec((int)fCurrentTime));
		}
	}
    //stops the game if ouf of time
	private void OutOfTime(float currentTime)
	{
		if (currentTime <= 0) {
			TimeScale.StopTime(true);
			Restart();
		}
	}
    //this convers the current time to a clocks string {00:00}
	private string ConvertToClockString(int Min,int Sec)
	{
		string sCurrentTime;
		sCurrentTime = Min.ToString() + ":" + Sec.ToString();
		return sCurrentTime;
	}
    //checks how minuts you got left
	private int CheckMinutes(int CurrentTime) 			
	{			
		CurrentTime = CurrentTime / 60;
		return CurrentTime;	
	}
    //Cheks how many seconds there is left
	private int CheckSec(int CurrentTime)
	{
		int iCurrentSec = CurrentTime % 60;
		return iCurrentSec;
	}
    //removes the rigt amout of time
	private float TimeToRemove(float fCurrentTime) 	
	{
		fCurrentTime -= TimeScale.DeltaTime;
		return fCurrentTime;
	}
}
