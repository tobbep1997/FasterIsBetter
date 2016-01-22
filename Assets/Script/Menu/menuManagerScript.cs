using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuManagerScript : MonoBehaviour {

	public GameObject startMenuPanel;
	public GameObject optionsMenuPanel;
	public GameObject levelSelectPanel;
    public GameObject ResetScoreWarning;
    public GameObject ControllSelection;

	public GameObject audioManager;

    //----------------------------------    Unity Standard Functions
	void Start () 
	{
		optionsMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
		optionsMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		optionsMenuPanel.GetComponent<CanvasGroup>().interactable = false;

		levelSelectPanel.GetComponent<CanvasGroup>().alpha = 0;
		levelSelectPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		levelSelectPanel.GetComponent<CanvasGroup>().interactable = false;

        ResetScoreWarning.GetComponent<CanvasGroup>().alpha = 0;
        ResetScoreWarning.GetComponent<CanvasGroup>().blocksRaycasts = false;
        ResetScoreWarning.GetComponent<CanvasGroup>().interactable = false;

        ControllSelection.GetComponent<CanvasGroup>().alpha = 0;
        ControllSelection.GetComponent<CanvasGroup>().blocksRaycasts = false;
        ControllSelection.GetComponent<CanvasGroup>().interactable = false;
    }	
	void Update () 
	{
        //clicking L will load to where the player was at its latest level
        if (Input.GetKeyDown (KeyCode.L)) {
            //for (int i = 0; i < PlayerPrefs.GetInt("GameSave1"); i++) {
            //Application.LoadLevel(i);
            //}
            //Application.LoadLevel(PlayerPrefs.GetInt("GameSave1"));
            SceneManager.LoadScene(PlayerPrefs.GetInt("GameSave1"));
        }

    }
    //----------------------------------    Select CanvasGroup
	public void exit_Button()
	{
		Application.Quit();
	}
	public void options_Button()
	{
		startMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
		startMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		startMenuPanel.GetComponent<CanvasGroup>().interactable = false;

        ResetScoreWarning.GetComponent<CanvasGroup>().alpha = 0;
        ResetScoreWarning.GetComponent<CanvasGroup>().blocksRaycasts = false;
        ResetScoreWarning.GetComponent<CanvasGroup>().interactable = false;

        ControllSelection.GetComponent<CanvasGroup>().alpha = 0;
        ControllSelection.GetComponent<CanvasGroup>().blocksRaycasts = false;
        ControllSelection.GetComponent<CanvasGroup>().interactable = false;

		optionsMenuPanel.GetComponent<CanvasGroup>().alpha = 1;
		optionsMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
		optionsMenuPanel.GetComponent<CanvasGroup>().interactable = true;


    }
	public void mainMenuPanel()
	{
		startMenuPanel.GetComponent<CanvasGroup>().alpha = 1;
		startMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
		startMenuPanel.GetComponent<CanvasGroup>().interactable = true;

		optionsMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
		optionsMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		optionsMenuPanel.GetComponent<CanvasGroup>().interactable = false;
		levelSelectPanel.GetComponent<CanvasGroup>().alpha = 0;
		levelSelectPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		levelSelectPanel.GetComponent<CanvasGroup>().interactable = false;
	}
	public void levelSelectPannel()
	{
		startMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
		startMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		startMenuPanel.GetComponent<CanvasGroup>().interactable = false;

		levelSelectPanel.GetComponent<CanvasGroup>().alpha = 1;
		levelSelectPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
		levelSelectPanel.GetComponent<CanvasGroup>().interactable = true;
	}
    public void Reset_Score_Warning()
    {
        optionsMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
        optionsMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        optionsMenuPanel.GetComponent<CanvasGroup>().interactable = false;

        ResetScoreWarning.GetComponent<CanvasGroup>().alpha = 1;
        ResetScoreWarning.GetComponent<CanvasGroup>().blocksRaycasts = true;
        ResetScoreWarning.GetComponent<CanvasGroup>().interactable = true;
    }
    public void Controll_Select_Panale()
    {
        ControllSelection.GetComponent<CanvasGroup>().alpha = 1;
        ControllSelection.GetComponent<CanvasGroup>().blocksRaycasts = true;
        ControllSelection.GetComponent<CanvasGroup>().interactable = true;

        optionsMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
        optionsMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        optionsMenuPanel.GetComponent<CanvasGroup>().interactable = false;
    }
    //----------------------------------    Load Levels
    public void LoadLatestLevel()
    {
        Instantiate(audioManager);
        TimeScale.ResetValues(false);
        if (PlayerPrefs.GetInt("GameSave1") < 3)
        {
            if (Application.platform != RuntimePlatform.WindowsEditor)                
                SceneManager.LoadScene(2);
            else
                SceneManager.LoadScene(3);
            
        }
        else
            SceneManager.LoadScene(PlayerPrefs.GetInt("GameSave1"));
    }
    public void Load_Latest_Level()
	{
		//Application.LoadLevel(PlayerPrefs.GetInt("GameSave1"));
        SceneManager.LoadScene(PlayerPrefs.GetInt("GameSave1"));
    } 
    //----------------------------------
}
	//public void levelOneButton()
	//{
	//	Instantiate (audioManager);
	//	TimeScale.ResetValues(false);
	//	//Application.LoadLevel("TutorialLevel");
    //  SceneManager.LoadScene("TutorialLevel");
    //}
