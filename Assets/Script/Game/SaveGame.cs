using UnityEngine; 
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
        
		if (PlayerPrefs.GetInt("GameSave1") < Application.loadedLevel) 
			PlayerPrefs.SetInt ("GameSave1", Application.loadedLevel);		
		PlayerPrefs.Save ();
       
        print(PlayerPrefs.GetInt("GameSave1") + " " + Application.loadedLevel + " " + PlayerPrefs.GetInt("HighScore"));
        
	}
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
