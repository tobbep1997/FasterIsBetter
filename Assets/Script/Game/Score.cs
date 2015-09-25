﻿using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public static int CombindScore;
    public static int ScoreMult = 4;
    public static int LevelScore;
    public UnityEngine.UI.Text ScoreText, TotalScoreText;



    public static void AddScore(int score)
    {
        LevelScore += score;
    }
    public static void FinishLevel(float TimeLeft)
    {
        LevelScore = (int)TimeLeft * ScoreMult;
        CombindScore = PlayerPrefs.GetInt("HighScore");
        if (PlayerPrefs.GetInt("GameSave1") <= Application.loadedLevel)
        {
            CombindScore += LevelScore;
        }
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
    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    void Update()
    {
        print(LevelScore);
        if (ScoreText == null || TotalScoreText == null)
        {
            return;
        }
        ScoreText.text = LevelScore.ToString();
        TotalScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
