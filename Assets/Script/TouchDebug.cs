using System;
using UnityEngine;

public class TouchDebug : MonoBehaviour
{
    public bool ShowTouch;

    void Update()
    {
        if (Input.touchCount >= 0)
        {
            foreach (Touch touch in Input.touches)
            { 
                
            }
        }
    
    }
}