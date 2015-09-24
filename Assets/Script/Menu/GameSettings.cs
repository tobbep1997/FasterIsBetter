using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

    [SerializeField]
    private GameObject[] menuButtons;

    private bool menuUp = false;

    public void StartMenu()
    {
        menuUp = true;
        DisplayButtons(menuUp);
        TimeScale.playing = false;
    }
    public void QuitMenu()
    {
        menuUp = false;
        DisplayButtons(menuUp);
        TimeScale.playing = true;
    }
    public void DisplayButtons(bool input)
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(input);
        }
    }
    public void Restart()
    {
        UIBehavior.Restart();
    }
    public void LoadMainMenu()
    {
        Application.LoadLevel("Main_Menu");
    }


}
