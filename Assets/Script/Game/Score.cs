using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public static int CombindScore;
    public static int ScoreMult = 4;

    
    public static void FinishLevel(float TimeLeft, bool DoubleScore)
    {
        int Score = (int)TimeLeft * ScoreMult;
        if (DoubleScore)        
            Score *= 2;
        CombindScore += Score;
        SaveScoreValues();
    }
    private static void SaveScoreValues()
    {
        PlayerPrefs.SetInt("HighScore", CombindScore);
        PlayerPrefs.Save();
    }
}
