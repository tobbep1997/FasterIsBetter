using UnityEngine;
using System.Collections;


public class ButtonController : MonoBehaviour {

    [SerializeField]
    private Controller_Type Buttons, Tatical_Buttons, Inversed_Tatical_Buttons;

    [SerializeField]
    private CharacterController CharController;

    private enum controllerType { Touch, Buttons, TactButton, InverTactButton }
    private controllerType controll_Type;

    private float currentDirrection = 0;

    [SerializeField]
    [Range(0,100)]
    float ButtonSizeScaleBasedOnScreenWidth, yPositionScale, xWidthBetweenButtons;



    //----------------------------  Unity Standard Fucntions
    void Start()
    {
        CharController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        Buttons.Enable(false);
        Tatical_Buttons.Enable(false);
        Inversed_Tatical_Buttons.Enable(false);

        switch (PlayerPrefs.GetInt("Controller_Type"))
        {
            case 0:
                controll_Type = controllerType.Touch;
                break;
            case 1:
                Buttons.Enable(true);
                controll_Type = controllerType.Buttons;
                break;
            case 2:
                Tatical_Buttons.Enable(true);
                controll_Type = controllerType.TactButton;
                break;
            case 3:
                Inversed_Tatical_Buttons.Enable(true);
                controll_Type = controllerType.InverTactButton;
                break;
        }
        SetButtonPosition();
    }


    void Update()
    {
        SetButtonPosition();
        switch (controll_Type)
        {
            case controllerType.Buttons:
                CheckButtons(Buttons.Left, Buttons.Right, Buttons.Jump);
                break;
            case controllerType.TactButton:
                CheckButtons(Tatical_Buttons.Left, Tatical_Buttons.Right, Tatical_Buttons.Jump);
                break;
            case controllerType.InverTactButton:
                CheckButtons(Inversed_Tatical_Buttons.Left, Inversed_Tatical_Buttons.Right, Inversed_Tatical_Buttons.Jump);
                break;
        }
    }

    private void SetButtonPosition()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float xWidth = screenWidth * (xWidthBetweenButtons / 100);        
        
        switch (controll_Type)
        {
            case controllerType.Buttons:

                Buttons.SetButtonScale(screenWidth * (ButtonSizeScaleBasedOnScreenWidth / 100));
                Buttons.SetYPos(screenHeight * (yPositionScale / 100));

                Buttons.Left.drawButton.Position.x = xWidth;
                Buttons.Jump.drawButton.Position.x = Buttons.Left.drawButton.Position.x + (Buttons.Jump.drawButton.Texture.width * Buttons.Jump.drawButton.Scale) + xWidth;
                Buttons.Right.drawButton.Position.x = screenWidth - (Buttons.Right.drawButton.Texture.width * Buttons.Right.drawButton.Scale) - xWidth;



                break;
            case controllerType.TactButton:

                Tatical_Buttons.SetButtonScale(screenWidth * (ButtonSizeScaleBasedOnScreenWidth / 100));
                Tatical_Buttons.SetYPos(screenHeight * (yPositionScale / 100));

                Tatical_Buttons.Jump.drawButton.Position.x = xWidth;
                Tatical_Buttons.Right.drawButton.Position.x = screenWidth - (Tatical_Buttons.Right.drawButton.Texture.width * Tatical_Buttons.Right.drawButton.Scale) - xWidth;
                Tatical_Buttons.Left.drawButton.Position.x = Tatical_Buttons.Right.drawButton.Position.x - (Tatical_Buttons.Left.drawButton.Texture.width * Tatical_Buttons.Left.drawButton.Scale) - xWidth;

                break;
            case controllerType.InverTactButton:

                Inversed_Tatical_Buttons.SetButtonScale(screenWidth * (ButtonSizeScaleBasedOnScreenWidth / 100));
                Inversed_Tatical_Buttons.SetYPos(screenHeight * (yPositionScale / 100));

                Inversed_Tatical_Buttons.Left.drawButton.Position.x = xWidth;
                Inversed_Tatical_Buttons.Right.drawButton.Position.x = Inversed_Tatical_Buttons.Left.drawButton.Position.x + (Inversed_Tatical_Buttons.Right.drawButton.Texture.width * Inversed_Tatical_Buttons.Right.drawButton.Scale) + xWidth;
                Inversed_Tatical_Buttons.Jump.drawButton.Position.x = screenWidth - (Inversed_Tatical_Buttons.Jump.drawButton.Texture.width * Inversed_Tatical_Buttons.Jump.drawButton.Scale) - xWidth;

                break;
        }
    }

    private void CheckButtons(ButtonInput left, ButtonInput right, ButtonInput jump)
    {
        if (jump.IsPressed)
        {
            CharController.JumpInput();
        }
        if (left.IsPressed)
        {
            if (!right.IsPressed)
                CharController.MoveLeft();
            else
                CharController.ResetDirection();
        }
        if (right.IsPressed)
        {
            if (!left.IsPressed)
                CharController.MoveRight();
            else
                CharController.ResetDirection();
        }
        if (!left.IsPressed && !right.IsPressed)
        {
            CharController.ResetDirection();
        }

    }
    private void PrintCurrentTouchPos()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            print(Input.touches[i].ToString() + new Vector2(Input.touches[i].position.x, ScreenReverseY(Input.touches[i].position.y)).ToString());
        }
    }
    private float ScreenReverseY(float Y)
    {
        return Screen.height - Y;
    }
}


[System.Serializable]
public class Controller_Type
{
    [SerializeField]
    private GameObject Group;
    [SerializeField]
    private ButtonInput left, right, jump, Interact;

    public void SetButtonScale(float scale)
    {
        left.drawButton.Scale = scale;
        jump.drawButton.Scale = scale;
        right.drawButton.Scale = scale;
    }
    public void SetYPos(float yPos)
    {
        left.drawButton.Position.y = yPos;
        jump.drawButton.Position.y = yPos;
        right.drawButton.Position.y = yPos;
    }

    public ButtonInput Left
    {
        get { return left; }
    }
    public ButtonInput Right
    {
        get { return right; }
    }
    public ButtonInput Jump
    {
        get { return jump; }
    }
    //----------------------------
    public void Enable(bool enable)
    {
        Group.SetActive(enable);
    }
    //----------------------------
}
