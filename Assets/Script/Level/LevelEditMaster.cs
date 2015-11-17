using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class LevelEditMaster : MonoBehaviour
{
    [SerializeField]
    private List<LevelEdit> LevelEdits;
    [SerializeField]
    private List<LevelEdit> LevelEditsWithColiders;

    [SerializeField]
    private bool ActivateTileCleaner, ForceTileUpdate;

    private void Update()
    {
        if (ActivateTileCleaner)
        {
            CleanTiles();
            //ActivateTileCleaner = false;
        }

        if (LevelEdits.Count <= 0 || ForceTileUpdate)
        {
            //ForceTileUpdate = false;
            GameObject[] tempObjs = GameObject.FindGameObjectsWithTag("Ground");
            LevelEdits = new List<LevelEdit>();

            foreach (GameObject item in tempObjs)
            {
                LevelEdit edit = item.GetComponent<LevelEdit>();
                if (edit != null)
                {
                    LevelEdits.Add(edit);
                }
            }
        }
        List<LevelEdit> removeEdits = new List<LevelEdit>();
        foreach (LevelEdit item in LevelEdits)
        {
            if (item == null)
            {
                removeEdits.Add(item);
            }
        }
        LevelEdit[] tempedits = removeEdits.ToArray();
        for (int i = 0; i < tempedits.Length; i++)
        {
            DestroyImmediate(tempedits[i]);
        }
    }

    private void CleanTiles()
    {
        List<Collider2D> collidersToRemove = new List<Collider2D>();
        foreach (LevelEdit tile in LevelEdits)
        {
            
            Collider2D col = tile.ReturnOtherColider();
            if (col != null)
            {
                collidersToRemove.Add(col);
            }
        }
        Collider2D[] Cols = collidersToRemove.ToArray();
        for (int i = 0; i < Cols.Length; i++)
        {
            DestroyImmediate(Cols[i].gameObject);
        }
    }
}
