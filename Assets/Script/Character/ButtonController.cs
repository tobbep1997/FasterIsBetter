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
                print(Tatical_Buttons.Jump.DisplayInformation());
                print(Tatical_Buttons.Left.DisplayInformation());
                print(Tatical_Buttons.Right.DisplayInformation());
                break;
            case 3:
                Inversed_Tatical_Buttons.Enable(true);
                controll_Type = controllerType.InverTactButton;
                break;
        }
    }
    void Update()
    {
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
        PrintCurrentTouchPos();
    }

    private void CheckButtons(ButtonInput left, ButtonInput right, ButtonInput jump)
    {
        if (jump.IsPressed)
        {
            CharController.JumpInput();
        }
        if (left.IsPressed)
        {
            CharController.MoveLeft();
        }
        if (right.IsPressed)
        {
            CharController.MoveRight();
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
    private ButtonInput left, right, jump;

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
        Group.GetComponent<CanvasGroup>().alpha = enable.ToInt();
        Group.GetComponent<CanvasGroup>().blocksRaycasts = enable;
        Group.GetComponent<CanvasGroup>().interactable = enable;
    }
    //----------------------------
}
