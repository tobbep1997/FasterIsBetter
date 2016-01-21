using UnityEngine;
using System.Collections;

public class inGameMenuManager : MonoBehaviour {

	public GameObject inGameMenuPanel;
	public GameObject inGameExitPanel;
	public GameObject inGameOptionsPanel;
	public GameObject inGameGrayPanel;

	bool showingInGameMenu = false;
	public bool showingInGameExitMenu = false;
	bool showingInGameOptionsMenu = false;
	// Use this for initialization
	void Start () 
	{
		inGameExitPanel.GetComponent<CanvasGroup>().alpha = 0;
		inGameExitPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		inGameExitPanel.GetComponent<CanvasGroup>().interactable = false;
		inGameMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
		inGameMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		inGameMenuPanel.GetComponent<CanvasGroup>().interactable = false;
		inGameOptionsPanel.GetComponent<CanvasGroup>().alpha = 0;
		inGameOptionsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		inGameOptionsPanel.GetComponent<CanvasGroup>().interactable = false;
		inGameGrayPanel.GetComponent<CanvasGroup>().alpha = 0;
		inGameGrayPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		inGameGrayPanel.GetComponent<CanvasGroup>().interactable = false;
	}

	// Update is called once per frames
	void Update () 
	{
		if (showingInGameMenu == false && Input.GetKeyDown(KeyCode.Escape)) 
		{
			showingInGameMenu = true;
		}
		else if (showingInGameMenu == true && Input.GetKeyDown(KeyCode.Escape)) 
		{
			showingInGameMenu = false;
		}
		inGameMenuPanelFunction();
	}
	void inGameMenuPanelFunction()
	{
		if (showingInGameMenu == true && showingInGameExitMenu == false && showingInGameOptionsMenu == false) 
		{
			inGameMenuPanel.GetComponent<CanvasGroup>().alpha = 1;
			inGameMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
			inGameMenuPanel.GetComponent<CanvasGroup>().interactable = true;
			inGameGrayPanel.GetComponent<CanvasGroup>().alpha = 1;
		}
		else if (showingInGameExitMenu == true || showingInGameOptionsMenu)
		{
			inGameMenuPanel.GetComponent<CanvasGroup>().alpha = 0.15f;
			inGameMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
			inGameMenuPanel.GetComponent<CanvasGroup>().interactable = false;
			inGameGrayPanel.GetComponent<CanvasGroup>().alpha = 1;
		}
		else 
		{
			inGameMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
			inGameMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
			inGameMenuPanel.GetComponent<CanvasGroup>().interactable = false;	
		}
	}
	public void resumeGameButton()
	{
		showingInGameMenu = false;
	}
	public void inGameExitGameButton()
	{
		showingInGameExitMenu= true;

		if (showingInGameExitMenu == true) 
		{
			inGameMenuPanel.GetComponent<CanvasGroup>().alpha = 0.15f;
			inGameMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
			inGameMenuPanel.GetComponent<CanvasGroup>().interactable = false;

			inGameExitPanel.GetComponent<CanvasGroup>().alpha = 1;
			inGameExitPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
			inGameExitPanel.GetComponent<CanvasGroup>().interactable = true;
		}
	}
	public void inGameExitGameButtonReturn()
	{
		showingInGameExitMenu = false;
		showingInGameOptionsMenu = false;

		inGameExitPanel.GetComponent<CanvasGroup>().alpha = 0;
		inGameExitPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		inGameExitPanel.GetComponent<CanvasGroup>().interactable = false;

		inGameOptionsPanel.GetComponent<CanvasGroup>().alpha = 0;
		inGameOptionsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		inGameOptionsPanel.GetComponent<CanvasGroup>().interactable = false;

	}
	public void inGameOptinsButton()
	{
		showingInGameOptionsMenu = true;

		if (showingInGameOptionsMenu) 
		{
			inGameMenuPanel.GetComponent<CanvasGroup>().alpha = 0.15f;
			inGameMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
			inGameMenuPanel.GetComponent<CanvasGroup>().interactable = false;
			
			inGameOptionsPanel.GetComponent<CanvasGroup>().alpha = 1;
			inGameOptionsPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
			inGameOptionsPanel.GetComponent<CanvasGroup>().interactable = true;
		}

	}
	public void inGameExitButtonQuitAppliction()
	{
		Application.Quit();
	}
	public void inGameExitButtonQuitMainMenu()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_menu");
	}
}
