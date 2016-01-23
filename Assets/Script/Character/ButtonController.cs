using UnityEngine;
using System.Collections;



public class ButtonController : MonoBehaviour {

    [SerializeField]
    private Controller_Type Buttons, Tatical_Buttons, Inversed_Tatical_Buttons;

    [SerializeField]
    private CharacterController CharController;

    private enum controllerType { Touch, Buttons, TactButton, InverTactButton }
    private controllerType controll_Type;

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
        if (left.IsPressed)
        {
            CharController.MoveLeft();
        }
        if (right.IsPressed)
        {
            CharController.MoveRight();
        }
        if (jump.IsPressed)
        {
            CharController.JumpInput();
        }
    }
    private void PrintCurrentTouchPos()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            print(Input.touches[i].ToString() + Input.touches[i].position.ToString());
        }
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
