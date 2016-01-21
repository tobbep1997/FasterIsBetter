using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

    [SerializeField]
    private GameObject[] menuButtons;

    //TODO: Make a PlayerPrefs that store if the player want buttons or not
    //TODO: Make sure to be able to switch between buttons and no buttons and make the buttons the default controller becuse people are noobs and dont learn 
    //and it realy start to piss me off
    //its not that hard plz

    private bool menuUp = false;

    public void StartMenu()
    {
        menuUp = true;
        DisplayButtons(menuUp);
        TimeScale.timeTicking = false;
        TimeScale.playing = false;
        
    }
    public void QuitMenu()
    {
        menuUp = false;
        TimeScale.timeTicking = true;
        TimeScale.playing = true;
        DisplayButtons(menuUp);        
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
        QuitMenu();
        UIBehavior.Restart();
    }
    public void LoadMainMenu()
    {
        QuitMenu();
        TimeScale.ResetValues(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
    }


}
