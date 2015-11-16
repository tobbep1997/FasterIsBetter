using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class LevelEditMaster : MonoBehaviour
{
    [SerializeField]
    private List<LevelEdit> LevelEdits;

    private void Update()
    {
        if (LevelEdits.Count <= 0)
        {
            GameObject[] tempObjs = GameObject.FindGameObjectsWithTag("Ground");

            foreach (GameObject item in tempObjs)
            {
                LevelEdit edit = item.GetComponent<LevelEdit>();
                if (!edit.Equals(null))
                {
                    LevelEdits.Add(edit);
                }
            }
        }


    }


}
