using UnityEngine;
using System.Collections;

public class TimeScale : MonoBehaviour {
    /// <summary>
    /// TimeScale is slowing down the time and after an anout stops the time
    /// </summary>
	public const int MaxTime = 1;           //This is the lowest timescale can become so the time aint speed up  
	public const float TimeToEnd = 5;       //this is the higest the timescale can go before the game stops

	public static float TimeToStopTime = 25;//the lower this value is the faster the time will slow
	public float TimeToStopTimeSetValue = 25;
	public static float Timer = 0;          //When this hits TimeTostopTime the game will stop and this is also the varbile that is lowerd when player picks up clock
    public static int TimeSteps = 3;
    public static int CurrentTimeStep;
    public static float TimeEveryTimeStep = 7;
    public static float LowestTimeScaleValue = -1;
    

	public static float fTimeScale = 1;     //The higher this value the slower time will go
	public static bool timeTicking = true;

	//this are the default values of the TimeScale DeltaTime and MaxDeltaTime
	private float newTimeScale;
	private static float fStarttimeScale;
	private static float fixedDeltaTime;
	private static float fixedMaximumDeltaTime;

	public static float DeltaTime;//becuse i broke delta time i have to use this one insted
	private static float PreviousUpdateTickTime;

	public static bool playing = true;//This script only update if this is true and sould only be accsese trough the StopTime(bool) fucntion
	private static bool WasPaused = false;
	private static bool isThereValues = false;

    private static float refValue;

	void Start()
	{
		fStarttimeScale = Time.timeScale;
		fixedDeltaTime = Time.fixedDeltaTime;
		fixedMaximumDeltaTime = Time.maximumDeltaTime;
		TimeToStopTime = TimeToStopTimeSetValue;
        
	}
	void Update()
	{
		//Checks if the game is playing
		if (playing)		
		{
            if (timeTicking)
            {
                UpdateTimes();  //Updates all the timeings
                SlowTime();     //Makes sure that the time slows down at the correct speed
            }
			DeltaTime = GetRealDelta(PreviousUpdateTickTime);//Updates deltaTime
			PreviousUpdateTickTime = Time.realtimeSinceStartup;
		} else
			Time.timeScale = 0;//if playing is paused this makes sure the game pauses
		CheckTime();//Check the time
		ReloadOnInput ();
	}
	void ReloadOnInput()//check if the player wants to restart 
	{
		if (Input.GetKeyDown(KeyCode.R)) {
			UIBehavior.Restart();
		}
	}
	void UpdateTimes()//this updates all the timings
	{
		Time.timeScale = fStarttimeScale/fTimeScale;
		Time.fixedDeltaTime = fixedDeltaTime/fTimeScale;
		Time.maximumDeltaTime = fixedMaximumDeltaTime/fTimeScale;
	}
	void SlowTime()//this makes sure the time slows down the correct amout based on time
	{
		Timer += DeltaTime;
        fTimeScale = Mathf.SmoothDamp(fTimeScale, ReturnTimeScaleStateValue(), ref refValue, .2f);
	}
	void CheckTime()//checks if the timescale has hit its mark and restart the game if it has
	{
		if (CurrentTimeStep >= TimeSteps) {		
			UIBehavior.Dead();					
		}
	}
	public static float ReturnTimeScaleStateValue()//this returns a value between 1 - MaxValue;
	{
        //float TimeScale = (Timer/TimeToStopTime)*TimeToEnd;
        //if (TimeScale < 1) {
        //	TimeScale = 1;
        //}
        float TimeToRemove = 1 - LowestTimeScaleValue;
        float RemoveTime = TimeToRemove / TimeSteps;
        int x = 0;
        for (int i = 0; i < TimeSteps + 1; i++)
        {
            if (Timer >= TimeEveryTimeStep * i)
            {
                x = i;
            }
        }
        CurrentTimeStep = x;

        float temp = RemoveTime * x;
        return 1 + temp;
        
        //return TimeScale;
	}
	public static void AddClocks(int clocks) {//This function is called from other classes to add time as when taking a clock
		Timer -= clocks * TimeEveryTimeStep;
	}
	public static void RemoveTime(float removeTime)//This function is called from other classes to remove time as when trying to contest an enemy
	{
		Timer += removeTime;
	}
	public static float GetRealDelta(float previousTime)//this return the right delta time relevant to the real world //Yea i know its cool there is a real world out there
	{
		float CurrentTime = Time.realtimeSinceStartup;
		float delta = CurrentTime- previousTime;
		return delta;
	}
	public static void StopTime(bool bStopTime)//this stops the time if the game needs to be paused
	{
		playing = !bStopTime;		
	}
	public static void ResetValues(bool ResetTime)//this resets all the values that needs to be reset and this sould be called everytime that you want to load a new scean or whatever that need to reset this class
	{
		if (ResetTime) {
			Time.timeScale = fStarttimeScale;
			Time.fixedDeltaTime = fixedDeltaTime;
			Time.maximumDeltaTime = fixedMaximumDeltaTime;
		}
		PreviousUpdateTickTime = Time.realtimeSinceStartup;
		Timer = 0;
		fTimeScale = 1;
	}
}
