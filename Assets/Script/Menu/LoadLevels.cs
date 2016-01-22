using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class LoadLevels : MonoBehaviour { 

    [SerializeField]
    private MapLevels[] mapLevels;

    [SerializeField]
    private GameObject audioManager;


    private void Start()
    {
        for (int i = 0; i < mapLevels.Length; i++)
        {
            for (int y = 0; y < mapLevels[i].LevelButtons.Length; y++)
            {
                mapLevels[i].LevelButtons[y].GetComponentInChildren<Text>().text = "Level " + (y + 1).ToString();
            }
        }

        for (int i = 0; i < mapLevels.Length; i++)
        {
            for (int y = 0; y < mapLevels[i].LevelButtons.Length; y++)
            {
                if (mapLevels[i].BuildIndexStart + y <= PlayerPrefs.GetInt("GameSave1"))
                {
                    if (y >= mapLevels[i].NumberOfLevels)
                    {
                        mapLevels[i].LevelButtons[y].interactable = false;
                    }
                    else
                        mapLevels[i].LevelButtons[y].interactable = true;
                }   
                else
                    mapLevels[i].LevelButtons[y].interactable = false;
            }
        }
    }

    public void OnButtonPress(Button butt)
    {
        for (int i = 0; i < mapLevels.Length; i++)
        {
            for (int y = 0; y < mapLevels[i].LevelButtons.Length; y++)
            {
                if (butt == mapLevels[i].LevelButtons[y])
                {
                    Instantiate(audioManager);
                    TimeScale.ResetValues(false);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(mapLevels[i].BuildIndexStart + y);
                }
            }
        }
    }
    public void OnButtonPress(string map, int level)
    {

    }


}
[Serializable]
public class MapLevels
{
    public string Map;
    public int BuildIndexStart;
    public int NumberOfLevels;

    public Button[] LevelButtons;
    
}
