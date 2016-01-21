using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

    public void LoadNextMap()
    {
        TimeScale.ResetValues(true);
        TimeScale.StopTime(false);
        TimeScale.playing = true;
        TimeScale.timeTicking = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void RestartLevel()
    {
        TimeScale.ResetValues(true);
        TimeScale.StopTime(false);
        TimeScale.playing = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}
