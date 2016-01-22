using UnityEngine;
using System.Collections;



public class ButtonController : MonoBehaviour {

    [SerializeField]
    private Controller_Type Buttons, Tatical_Buttons, Inversed_Tatical_Buttons;


    //----------------------------  Unity Standard Fucntions
    void Start()
    {
        Buttons.Enable(false);
        Tatical_Buttons.Enable(false);
        Inversed_Tatical_Buttons.Enable(false);

        switch (PlayerPrefs.GetInt("Controller_Type"))
        {
            case 1:
                Buttons.Enable(true);
                break;
            case 2:
                Tatical_Buttons.Enable(true);
                break;
            case 3:
                Inversed_Tatical_Buttons.Enable(true);
                break;
        }
    }	
}


[System.Serializable]
public class Controller_Type
{
    [SerializeField]
    private GameObject Group;
    [SerializeField]
    private ButtonInput Left, Right, Jump;

    private CharacterController charController;

    //----------------------------
    private bool InitComfirmed = false;
    public void Init(CharacterController charController)
    {
        this.charController = charController;
        InitComfirmed = true;
        Debug.Log("Init sucssesful");
    }
    //----------------------------
    public void Enable(bool enable)
    {                
        Group.GetComponent<CanvasGroup>().alpha = enable.ToInt();
        Group.GetComponent<CanvasGroup>().blocksRaycasts = enable;
        Group.GetComponent<CanvasGroup>().interactable = enable;
    }
    //----------------------------
    public void Update()
    {
        if (!UpdateCheck())
            return;
    }
    //----------------------------  Debug
    private bool UpdateCheck()
    {
        if (!InitComfirmed)
        {
            Debug.Log("Controller not initialized");
            return false;
        }
        if (Left == null || Right == null || Jump == null)
        {
            Debug.Log("Not all buttons where assigned correctly");
            return false;
        }
        if (Group == null)
        {
            Debug.Log("Canvas was not assigned correcly");
            return false;
        }
        return true;
    }


}
