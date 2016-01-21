using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public static int HighScore;
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
        HighScore = PlayerPrefs.GetInt(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.ToString() + "HighScore");
        if (LevelScore > HighScore)
        {
            HighScore = LevelScore;
        }
        SaveScoreValues();
    }
    public static void DobuleScore()
    {
        LevelScore *= 2;
        HighScore = PlayerPrefs.GetInt(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.ToString() + "HighScore");
        if (LevelScore > HighScore)
        {
            HighScore = LevelScore;
        }
        SaveScoreValues();
    }
    private static void SaveScoreValues()
    {
        PlayerPrefs.SetInt(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.ToString() + "HighScore", HighScore);
        PlayerPrefs.Save();
    }
    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    void Update()
    {

        if (ScoreText == null || TotalScoreText == null)
        {
            return;
        }
        ScoreText.text = LevelScore.ToString();
        TotalScoreText.text = PlayerPrefs.GetInt(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.ToString() + "HighScore").ToString();
    }
}
