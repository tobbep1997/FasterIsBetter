using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

    public void LoadNextMap()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
        TimeScale.timeTicking = true;
    }

}
