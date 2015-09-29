using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System.Collections;
using System;

public class UnityAds : MonoBehaviour
{

    public double CoolDownInMinuts;

    private DateTime timeDone;
    public void ShowAd()
    {
        if (Advertisement.IsReady() && DateTime.Now > timeDone)
        {
            print("AD");
            timeDone = DateTime.Now;
            timeDone = timeDone.AddMinutes(CoolDownInMinuts);
            Advertisement.Show();
            Score.DobuleScore();
        }
    }
    public void Update()
    {
        print(timeDone);
        print(DateTime.Now);
    }
}
