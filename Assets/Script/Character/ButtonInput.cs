using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour {

    [SerializeField]
    private RectTransform ButtonRect;
    private bool UsingButtons;
    [SerializeField]
    private UIRectangle UIRect;
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
        Vector2 temp = ButtonRect.rect.position.ReturnScreenPosition(Camera.main);
        UIRect = new UIRectangle(temp);
        //UIRect = new UIRectangle(temp, new Vector2(ButtonRect.rect.position.x + ButtonRect.rect.width, ButtonRect.rect.position.y).ReturnScreenPosition(Camera.main).x - temp.x,
        //                               new Vector2(ButtonRect.rect.position.x, ButtonRect.rect.position.y + ButtonRect.rect.height).ReturnScreenPosition(Camera.main).y - temp.y);     
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
                if (UIRect.ContainsVector(new Vector2(Input.touches[i].position.x, ScreenReverseY(Input.touches[i].position.y))))
                {
                    _isPressed = true;
                    print("pressed " + gameObject.name);
                    break;
                }          
                else
                    _isPressed = false;      
            }
        }
        else
        {
            _isPressed = false;
        }
    }
    private float ScreenReverseY(float Y)
    {
        return Screen.height - Y;
    }
    //--------------------------------------------------
    public string DisplayInformation()
    {
        return gameObject.name + " " + UIRect.Position.ToString();
    }

}
