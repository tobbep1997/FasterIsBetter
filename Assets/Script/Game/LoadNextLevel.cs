using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

    public void LoadNextMap()
    {
        TimeScale.ResetValues(true);
        TimeScale.StopTime(false);
        TimeScale.playing = true;
        TimeScale.timeTicking = true;
        Application.LoadLevel(Application.loadedLevel + 1);

    }
    public void RestartLevel()
    {
        TimeScale.ResetValues(true);
        TimeScale.StopTime(false);
        TimeScale.playing = true;
        Application.LoadLevel(Application.loadedLevel);
    }

}
