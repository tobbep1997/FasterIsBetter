using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetControlls : MonoBehaviour {

    //------------------------------------
    [SerializeField]
    [Range(0,3)]
    private int controller_Type = 1;
    //0 = touch
    //1 = buttons
    //2 = tatical buttons
    //3 = inverted tatical buttons

    [SerializeField]
    Button Touch, Buttons, Tactical, Inverted;

    //------------------------------------  Unity Standard Functions
    void Start()
    {
        controller_Type = PlayerPrefs.GetInt("Controller_Type");
        SetInteracteble();
    }
    //------------------------------------  Set Controller type
    void SetInteracteble()
    {
        Touch.interactable = true;
        Buttons.interactable = true;
        Tactical.interactable = true;
        Inverted.interactable = true;

        switch (controller_Type)
        {
            case 0:
                Touch.interactable = false;
                break;
            case 1:
                Buttons.interactable = false;
                break;
            case 2:
                Tactical.interactable = false;
                break;
            case 3:
                Inverted.interactable = false;
                break;
            default:
                break;
        }
    }
    public void Set_Touch()
    {
        controller_Type = 0;
        Save_Current_Controller();
        SetInteracteble();
    }
    public void Set_Buttons()
    {
        controller_Type = 1;
        Save_Current_Controller();
        SetInteracteble();
    }
    public void Set_Tatical()
    {
        controller_Type = 2;
        Save_Current_Controller();
        SetInteracteble();
    }
    public void Set_Inverted()
    {
        controller_Type = 3;
        Save_Current_Controller();
        SetInteracteble();
    }
    //------------------------------------  Save the current controller type
    private void Save_Current_Controller()
    {
        PlayerPrefs.SetInt("Controller_Type", controller_Type);
        PlayerPrefs.Save();
    }
}
