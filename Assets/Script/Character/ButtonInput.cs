using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour {

    [SerializeField]
    private RectTransform ButtonRect;
    private bool UsingButtons;
    [SerializeField]
    private bool _isPressed;
    public bool IsPressed
    {
        get { return _isPressed; }
    } 

    //--------------------------------------------------    Unity Standard Functions
    private void Start()
    {
        switch (PlayerPrefs.GetInt("Controller_Type"))
        {
            case 0:
                UsingButtons = false;
                break;
            case 1:
            case 2:
            case 3:
                UsingButtons = true;
                break;
        }
        //Button.alpha = 0;
        //Button.blocksRaycasts = false;
        //Button.interactable = false;

        //Tatical_Button.alpha = 0;
        //Tatical_Button.blocksRaycasts = false;
        //Tatical_Button.interactable = false;

        //Inversed_Tatical_Button.alpha = 0;
        //Inversed_Tatical_Button.blocksRaycasts = false;
        //Inversed_Tatical_Button.interactable = false;

        //switch (PlayerPrefs.GetInt("Controller_Type"))
        //{
        //    case 0:
        //        UsingButtons = false;
        //        break;
        //    case 1:
        //        UsingButtons = true;
        //        Button.alpha = 1;
        //        Button.blocksRaycasts = true;
        //        Button.interactable = true;
        //        break;
        //    case 2:
        //        UsingButtons = true;
        //        Button.alpha = 1;
        //        Button.blocksRaycasts = true;
        //        Button.interactable = true;
        //        break;
        //    case 3:
        //        UsingButtons = true;
        //        Button.alpha = 1;
        //        Button.blocksRaycasts = true;
        //        Button.interactable = true;
        //        break;
        //}
    }
    private void Update()
    {
        if (!UsingButtons)
            return;        
        Check_Touch_Position_Within_ButtonRect();
    }
    //--------------------------------------------------    Button main fuction
    private void Check_Touch_Position_Within_ButtonRect()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (ButtonRect.rect.ContainsVector(Input.touches[i].position))
                {
                    _isPressed = true;
                    print("pressed");
                    break;
                }          
                else
                    _isPressed = false;      
            }
        }
    }
}
