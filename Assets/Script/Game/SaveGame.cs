﻿using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public class SaveGame : MonoBehaviour {
    /// <summary>
    /// SaveGames asures the player the abilety to save the game just by progressing in the game
    /// </summary>

	public int levelSave = 0;



	void Start () {
        
		if (PlayerPrefs.GetInt("GameSave1") < UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex) 
			PlayerPrefs.SetInt ("GameSave1", UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);		
		PlayerPrefs.Save ();        
	}
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
