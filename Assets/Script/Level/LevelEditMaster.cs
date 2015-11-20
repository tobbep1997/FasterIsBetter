using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class LevelEditMaster : MonoBehaviour
{
    [SerializeField]
    private List<LevelEdit> LevelEdits;

    [SerializeField]
    private bool activateTileCleaner, forceTileUpdate, enableAllColiders, updateCollidersToUse;

    private void Update()
    {
        CleanTiles();
        UpdateTiles();
        EnableAllColiders();
        UpdateColliders();    
    }

    private void CleanTiles()
    {
        if (activateTileCleaner)
        {
            activateTileCleaner = false;
            forceTileUpdate = true;
        }
        else
            return;

        LevelEdit[] tempEdits = LevelEdits.ToArray();

        for (int i = 0; i < tempEdits.Length; i++)
        {
            if (tempEdits[i] == null)
                continue;

            Collider2D col = tempEdits[i].ReturnOtherColider();
            if (col != null)
            {
                DestroyImmediate(col.gameObject);
            }
        }
    }
    private void UpdateTiles()
    {
        if (LevelEdits.Count <= 0 || forceTileUpdate)
        {
            forceTileUpdate = false;
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

        
    }
    private void RemoveNullTiles()
    {
        List<LevelEdit> removeEdits = new List<LevelEdit>();
        foreach (LevelEdit item in LevelEdits)
        {
            if (item == null)
            {
                removeEdits.Add(item);
            }
        }

        LevelEdit[] tempEdits = removeEdits.ToArray();
        for (int i = 0; i < tempEdits.Length; i++)
        {
            DestroyImmediate(tempEdits[i]);
        }
    }
    private void EnableAllColiders()
    {
        if (!enableAllColiders)
        {
            return;
        }
        else
        {
            enableAllColiders = false;
        }

        foreach (LevelEdit item in LevelEdits)
        {
            item.CurrentCollider.enabled = true;
        }
    }
    private void UpdateColliders()
    {
        if (!updateCollidersToUse)
        {
            return;
        }
        else
        {
            updateCollidersToUse = false;
        }
        foreach (LevelEdit item in LevelEdits)
        {
            item.UpdateColliderCheck();
        }
        foreach (LevelEdit item in LevelEdits)
        {
            item.CurrentCollider.enabled = !item.UseCollider;
        }

    }
}


