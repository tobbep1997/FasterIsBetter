using UnityEngine;

[RequireComponent(typeof(DrawButton))]
public class ButtonInput : MonoBehaviour {

    private bool UsingButtons;
    [SerializeField]
    private UIRectangle UIRect;
    [SerializeField]
    private bool _isPressed;
    public bool IsPressed
    {
        get { return _isPressed; }
    }

    public DrawButton drawButton;

    //--------------------------------------------------    Unity Standard Functions
    private void Start()
    {
        drawButton = GetComponent<DrawButton>();

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

        UIRect = new UIRectangle(drawButton.Position, drawButton.Texture.width * drawButton.Scale, drawButton.Texture.height * drawButton.Scale);
    }
    private void Update()
    {

        UIRect = new UIRectangle(drawButton.Position, drawButton.Texture.width * drawButton.Scale, drawButton.Texture.height * drawButton.Scale);

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
                if (UIRect.ContainsVector(new Vector2(Input.touches[i].position.x,ScreenReverseY(Input.touches[i].position.y))))
                {
                    _isPressed = true;
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
