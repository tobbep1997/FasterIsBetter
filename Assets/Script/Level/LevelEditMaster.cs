using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

[ExecuteInEditMode]
public class LevelEditMaster : MonoBehaviour
{
    [SerializeField]
    private List<LevelEdit> LevelEdits;
    [SerializeField]
    private List<ColliderOnTiles> CollidersOnTiles;

    [SerializeField]
    private GameObject ColliderGameobject;

    [SerializeField]
    private bool Start, Revers;
    [SerializeField]
    AdminControlls adminControlls;
    //private bool activateTileCleaner, forceTileUpdate, enableAllColiders, RemoveColliders, getNewColliders, CreateNewCollider;

    private void Update()
    {
        if (Start)
        {
            if (ColliderGameobject == null)
            {
                Debug.LogError("The ColliderGameobject is null. Can not proceed with tile cleaning");                
                Start = false;
                return;
            }
            Start = false;

            adminControlls.forceTileUpdate = true;
            adminControlls.getNewColliders = true;
            adminControlls.RemoveColliders = true;
            adminControlls.enableAllColiders = true;
            adminControlls.CreateNewCollider = true;
            adminControlls.activateTileCleaner = true;
        }
        ReversSettings();

        UpdateTiles();
        EnableAllColiders();
        CleanTiles();        


        UpdateColliders();
        GetNewColliders();
        CreateNewColliders();

        DrawColliderLine();

        RemoveNullTiles();
    }


    private void CleanTiles()
    {
        if (adminControlls.activateTileCleaner)
        {
            adminControlls.activateTileCleaner = false;
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
        if (LevelEdits.Count <= 0 || adminControlls.forceTileUpdate)
        {
            adminControlls.forceTileUpdate = false;
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
        if (!adminControlls.enableAllColiders)
        {
            return;
        }
        else
        {
            adminControlls.enableAllColiders = false;
        }

        foreach (LevelEdit item in LevelEdits)
        {
            item.CurrentCollider.enabled = true;
        }
    }
    private void UpdateColliders()
    {
        if (!adminControlls.RemoveColliders)
        {
            return;
        }
        else
        {
            adminControlls.RemoveColliders = false;
        }
        foreach (LevelEdit item in LevelEdits)
        {
            item.CurrentCollider.enabled = !item.UseCollider;
        }
    }

    private void GetNewColliders()
    {
        if (adminControlls.getNewColliders)
        {
            adminControlls.getNewColliders = false;
        }
        else
            return;

        LevelEdit[] edits = LevelEdits.ToArray();
        List<LevelEdit> UsedEdits = new List<LevelEdit>();
        CollidersOnTiles = new List<ColliderOnTiles>();

        for (int i = 0; i < edits.Length; i++)
        {
            if (!edits[i].GetComponent<Collider2D>().enabled)
            {
                continue;   
            }
            //Vertical
            LevelEdit[] temp = edits[i].GetAllEditsInDir(Mathf.PI / 2, UsedEdits.ToArray());
            if (temp == null)
            {
                continue;
            }
            CollidersOnTiles.Add(new ColliderOnTiles(temp));

            for (int x = 0; x < temp.Length; x++)
            {
                UsedEdits.Add(temp[x]);
            }           
        }
        for (int i = 0; i < edits.Length; i++)
        {
            if (!edits[i].GetComponent<Collider2D>().enabled)
            {
                continue;
            }
            //Horizontal
            LevelEdit[] temp = edits[i].GetAllEditsInDir(0, UsedEdits.ToArray());
            if (temp == null)
            {
                continue;
            }
            CollidersOnTiles.Add(new ColliderOnTiles(temp));

            for (int x = 0; x < temp.Length; x++)
            {
                UsedEdits.Add(temp[x]);
            }

        }

    }
    private void DrawColliderLine()
    {
        foreach (var levelEdit in CollidersOnTiles)
        {
            levelEdit.DrawColliderLines(ColliderOnTiles.Angel.Vertical);
            levelEdit.DrawColliderLines(ColliderOnTiles.Angel.Horizontal);
        }
    }
    private void CreateNewColliders()
    {
        if (!adminControlls.CreateNewCollider)
        {
            return;
        }
        else
            adminControlls.CreateNewCollider = false;

        foreach (var item in CollidersOnTiles)
        {
            item.CreateNewColliders(ColliderGameobject);
            item.RemoveOriginalColliders();
        }
    }

    private void ReversSettings()
    {
        if (Revers)
        {
            Revers = false;
        }
        else
            return;

        Collider2D[] cols = ColliderGameobject.GetComponents<Collider2D>();

        for (int i = 0; i < cols.Length; i++)
        {
            DestroyImmediate(cols[i]);
        }

        adminControlls.enableAllColiders = true;
    }

}
[System.Serializable]
public class ColliderOnTiles
{
    public LevelEdit[] Edits;
    public BoxCollider2D Colliders;

    public enum Angel { Horizontal, Vertical }    

    public ColliderOnTiles(LevelEdit[] Edits)
    {
        this.Edits = Edits;
    }

    public void DrawColliderLines(Angel angel)
    {
        Vector2 max = Vector2.zero, min  = Vector2.zero;

        switch (angel)
        {
            case Angel.Horizontal:
                for (int i = 0; i < Edits.Length; i++)
                {
                    if (Edits[i].CurrentCollider.bounds.center.x > max.x || max == Vector2.zero)
                    {
                        max = Edits[i].CurrentCollider.bounds.center;                        
                    }

                    if (Edits[i].CurrentCollider.bounds.center.x < min.x || min == Vector2.zero)
                    {
                        min = Edits[i].CurrentCollider.bounds.center;
                    }
                }
                Debug.DrawLine(max, min, Color.cyan);
                break;
            case Angel.Vertical:
                for (int i = 0; i < Edits.Length; i++)
                {
                    if (Edits[i].CurrentCollider.bounds.center.y > max.y || max == Vector2.zero)
                    {
                        max = Edits[i].CurrentCollider.bounds.center;
                    }

                    if (Edits[i].CurrentCollider.bounds.center.y < min.y || min == Vector2.zero)
                    {
                        min = Edits[i].CurrentCollider.bounds.center;
                    }
                }
                Debug.DrawLine(max, min, Color.cyan);
                break;
        }
        Vector3 sum = Vector3.zero;
        for (int i = 0; i < Edits.Length; i++)
        {
            sum += Edits[i].CurrentCollider.bounds.center;
        }

        sum /= Edits.Length;
        Ray2D ray;
        //RaycastHit2D[] hits;
        for (int i = 0; i < Edits.Length; i++)
        for (float x = 0; x <= Mathf.PI * 2; x += Mathf.PI / 2)
        {
            ray = new Ray2D(sum, new Vector2(Mathf.Cos(x), Mathf.Sin(x)));

            Debug.DrawRay(ray.origin, ray.direction * Edits[i].CurrentCollider.bounds.extents.x/2, Color.red);
        }
        }
    public void CreateNewColliders(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        float MaxX = float.NaN, MinX = float.NaN, MaxY = float.NaN, MinY = float.NaN;       

        for (int i = 0; i < Edits.Length; i++)
        {
            if (Edits[i].CurrentCollider.bounds.max.x > MaxX || float.IsNaN(MaxX))
            {
                MaxX = Edits[i].CurrentCollider.bounds.max.x;
            }
            if (Edits[i].CurrentCollider.bounds.min.x < MinX|| float.IsNaN(MinX))
            {
                MinX = Edits[i].CurrentCollider.bounds.min.x;
            }

            if (Edits[i].CurrentCollider.bounds.max.y > MaxY || float.IsNaN(MaxY))
            {
                MaxY = Edits[i].CurrentCollider.bounds.max.y;
            }
            if (Edits[i].CurrentCollider.bounds.min.y < MinY || float.IsNaN(MinY))
            {
                MinY = Edits[i].CurrentCollider.bounds.min.y;
            }
        }
        
        Colliders = obj.AddComponent<BoxCollider2D>();
        Colliders.size = new Vector2(MaxX - MinX, MaxY - MinY);

        Vector3 sum = Vector3.zero;
        for (int i = 0; i < Edits.Length; i++)
        {
            sum += Edits[i].CurrentCollider.bounds.center;
        }

        sum /= Edits.Length;

        Vector3 dir = obj.transform.position + sum;

        Colliders.offset = dir;//.normalized * Vector2.Distance(obj.transform.position, sum);

    }
    public void RemoveOriginalColliders()
    {
        for (int i = 0; i < Edits.Length; i++)
        {
            Edits[i].CurrentCollider.enabled = false;
        }
    }

}
[System.Serializable]
public class AdminControlls
{    
    public bool activateTileCleaner, forceTileUpdate, enableAllColiders, RemoveColliders, getNewColliders, CreateNewCollider;
}


