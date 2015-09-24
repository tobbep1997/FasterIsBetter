using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public static int CombindScore;
    public static int ScoreMult = 4;
    public static int LevelScore;
    public UnityEngine.UI.Text ScoreText,TotalScoreText;

    
    public static void FinishLevel(float TimeLeft)
    {
        LevelScore = (int)TimeLeft * ScoreMult;     
        if (PlayerPrefs.GetInt("GameSave1") >= Application.loadedLevel)
            CombindScore += LevelScore;
        SaveScoreValues();
    }
    public static void DobuleScore()
    {       
        LevelScore *= 2;
    }
    private static void SaveScoreValues()
    {
        PlayerPrefs.SetInt("HighScore", CombindScore);
        PlayerPrefs.Save();
    }
    void Update()
    {
        ScoreText.text = LevelScore.ToString();
        TotalScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
