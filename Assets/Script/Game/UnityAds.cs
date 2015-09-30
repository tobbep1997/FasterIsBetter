using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System.Collections;
using System;

public class UnityAds : MonoBehaviour
{

    public double CoolDownInMinuts;

    private DateTime timeDone;

    [SerializeField]
    private Button button;
    [SerializeField]
    private Text timerText;
    private bool displayButton;

    private void Start()
    {
        timeDone = new DateTime(SetTime());
    }

    public void ShowAd()
    {
#if UNITY_IPHONE

#endif
#if UNITY_ANDROID

#endif
        timeDone = new DateTime(SetTime());
        if (Advertisement.IsReady() && DateTime.Now > timeDone)
        {
            timeDone = DateTime.Now;
            timeDone = timeDone.AddMinutes(CoolDownInMinuts);            
            Advertisement.Show();
            Score.DobuleScore();            
            SaveTime();
        }

       
        
    }
    private void SaveTime()
    {
        PlayerPrefs.SetString("TimeTicks", timeDone.Ticks.ToString());
        PlayerPrefs.Save();
    }
    private long SetTime()
    {
        return System.Convert.ToInt64(PlayerPrefs.GetString("TimeTicks","0"));
    }
    public void Update()
    {
        timeDone = new DateTime(SetTime());
        if (DateTime.Now > timeDone)
        {
            displayButton = true;
        }
        else
            displayButton = false;
        if (!displayButton)
        {
            timerText.enabled = true;
            TimeSpan temp = timeDone - DateTime.Now;
            timerText.text = ConvertToClockString(temp.Minutes, temp.Seconds);
        }
        else
            timerText.enabled = false;
        button.interactable = displayButton;
    }
    //this convers the current time to a clocks string {00:00}
    private string ConvertToClockString(int Min, int Sec)
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
}
