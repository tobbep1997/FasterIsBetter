using UnityEngine;
using System.Collections;

public class SetControlls : MonoBehaviour {

    //------------------------------------
    [SerializeField]
    [Range(0,3)]
    private int controller_Type = 1;
    //0 = touch
    //1 = buttons
    //2 = tatical buttons
    //3 = inverted tatical buttons

    //------------------------------------  Unity Standard Functions
    void Start()
    {
        controller_Type = PlayerPrefs.GetInt("Controller_Type");
    }
    //------------------------------------  Set Controller type
    public void Set_Touch()
    {
        controller_Type = 0;
        Save_Current_Controller();
    }
    public void Set_Buttons()
    {
        controller_Type = 1;
        Save_Current_Controller();
    }
    public void Set_Tatical()
    {
        controller_Type = 2;
        Save_Current_Controller();
    }
    public void Set_Inverted()
    {
        controller_Type = 3;
        Save_Current_Controller();
    }
    //------------------------------------  Save the current controller type
    private void Save_Current_Controller()
    {
        PlayerPrefs.SetInt("Controller_Type", controller_Type);
        PlayerPrefs.Save();
    }
}
