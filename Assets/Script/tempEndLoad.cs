﻿using UnityEngine;
using System.Collections;

public class tempEndLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadMenu()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_menu");
	}
}
