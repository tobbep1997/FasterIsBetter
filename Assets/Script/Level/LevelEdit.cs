using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]//This makes sure that the script is always running
[System.Serializable]
public class LevelEdit : MonoBehaviour {	
    /// <summary>
    /// This makes it easier to make levels and makes sure that everything is connected to a grid
    /// </summary>
	public Vector2 vCell_size;                      //this is the size of the tile that this script is on
	//public Vector2 vCell_Offset;
	private Vector2 vCurrentPos,vPreviousPos;       //This is the current and the previous position of the tile

    [SerializeField]
    private Collider2D _CurrentCollider;
    public Collider2D CurrentCollider
    {
        get { return _CurrentCollider; }
        //set { _CurrentCollider = value; }
    }
    private bool _UseCollider;
    public bool UseCollider
    {
        get { return _UseCollider; }
    }

    private void Start()
    {
        _CurrentCollider = GetComponent<Collider2D>();
        
    }
	void Update ()  
    {
        SetPosition();

        UpdateColliderCheck();

        if (_CurrentCollider == null)
        {
            _CurrentCollider = GetComponent<Collider2D>();
            if (_CurrentCollider == null)
            {
                DestroyImmediate(gameObject);
            }
        }
	}

    public void UpdateColliderCheck()
    {
        _UseCollider = CheckIfObjectsAllAround();
    }

    private void SetPosition()
    {
        vCurrentPos = transform.position;
        //This makes sure the tile is moved it will snap to the grid apropiet to the size of the tile
        if (vCurrentPos != vPreviousPos)
        {
            transform.position = new Vector3((Mathf.Round(transform.position.x / (vCell_size.x)) * (vCell_size.x)),
                                             (Mathf.Round(transform.position.y / (vCell_size.y)) * (vCell_size.y)),
                                             transform.position.z);
        }
        vPreviousPos = vCurrentPos;
    }
    public bool CheckIfObjectsAllAround()//if returns false there sould be a colider on it
    {
        if (_CurrentCollider.isTrigger)
            return true;

        Ray2D ray;
        RaycastHit2D[] hits;

        for (float i = 0; i <= Mathf.PI * 2; i += Mathf.PI/2)
        {
            ray = new Ray2D(_CurrentCollider.bounds.center, new Vector2(Mathf.Cos(i), Mathf.Sin(i)));
            hits = Physics2D.RaycastAll(_CurrentCollider.bounds.center, ray.direction, Mathf.Sqrt(_CurrentCollider.bounds.extents.sqrMagnitude));

            //Debug.DrawRay(CurrentCollider.bounds.center, ray.direction * CurrentCollider.bounds.extents.x*2, Color.red);

            bool IntersectOtherObject = false;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject != gameObject && !hit.collider.isTrigger)
                {
                    IntersectOtherObject = true;
                    break;
                }                
            }
            if (!IntersectOtherObject)
            {
                //Debug.DrawRay(_CurrentCollider.bounds.center, ray.direction * Mathf.Sqrt(_CurrentCollider.bounds.extents.sqrMagnitude), Color.green);
                return false;
            }
        }
        return true;
    }
    public Collider2D ReturnOtherColider()//This returns the other gameobjects collider if there is a tile on the same place at this tile
    {
        Ray2D ray;
        RaycastHit2D[] hits;
        for (float i = 0; i <= Mathf.PI * 2; i += Mathf.PI / 2)
        {
            ray = new Ray2D(_CurrentCollider.bounds.center, new Vector2(Mathf.Cos(i), Mathf.Sin(i)));
            hits = Physics2D.RaycastAll(_CurrentCollider.bounds.center, ray.direction, Mathf.Sqrt(_CurrentCollider.bounds.extents.sqrMagnitude) / 2);
            //Debug.DrawRay(ray.origin, ray.direction * Mathf.Sqrt(_CurrentCollider.bounds.extents.sqrMagnitude) / 2);
        }
        for (float i = 0; i <= Mathf.PI * 2; i += Mathf.PI / 2)
        {
            ray = new Ray2D(_CurrentCollider.bounds.center, new Vector2(Mathf.Cos(i), Mathf.Sin(i)));
            hits = Physics2D.RaycastAll(_CurrentCollider.bounds.center, ray.direction, Mathf.Sqrt(_CurrentCollider.bounds.extents.sqrMagnitude) / 2);

            for (int x = 0; x < hits.Length; x++)
            {
                if (hits[x].transform.gameObject != gameObject && !hits[x].collider.isTrigger)
                {
                    return hits[x].collider;
                }
            }

            //foreach (RaycastHit2D hit in hits)
            //{
            //    if (hit.transform.gameObject != gameObject && !hit.collider.isTrigger)
            //    {
            //        return hit.collider;
            //    }
            //}
        }
        return null;
    }

    public LevelEdit CheckIfTileInDir(float rad)
    {
        Ray2D ray;
        RaycastHit2D[] hits;      

        ray = new Ray2D(_CurrentCollider.bounds.center, new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)));
        hits = Physics2D.RaycastAll(_CurrentCollider.bounds.center, ray.direction, Mathf.Sqrt(_CurrentCollider.bounds.extents.sqrMagnitude));

        //Debug.DrawRay(ray.origin, ray.direction * Mathf.Sqrt(_CurrentCollider.bounds.extents.sqrMagnitude));
       
        for (int x = 0; x < hits.Length; x++)
        {
            if (hits[x].transform.gameObject != gameObject && !hits[x].collider.isTrigger && hits[x].collider.enabled)
            {
                return hits[x].transform.gameObject.GetComponent<LevelEdit>();
            }
        }
        return null;

    }
    public LevelEdit[] GetAllEditsInDir(float rad, LevelEdit[] UsedEdits)
    {
        LevelEdit Current;
        List<LevelEdit> Edits = new List<LevelEdit>();

        if (UsedEdits.AnyInstanceIsEqual<LevelEdit>(this))
        {
            return null;
        }

        Current = this;
        
        do
        {            
            Current = Current.CheckIfTileInDir(rad);

            if (Current != null && !UsedEdits.AnyInstanceIsEqual<LevelEdit>(Current))
            {
                Edits.Add(Current);
            }
        }   while (Current != null);

        Current = this;

        do
        {
            Current = Current.CheckIfTileInDir(rad + Mathf.PI);

            if (Current != null && !UsedEdits.AnyInstanceIsEqual<LevelEdit>(Current))
            {
                Edits.Add(Current);
            }
        }   while (Current != null);

        if (Edits.Count > 0)
        {
            Edits.Add(this);
            return Edits.ToArray();
        }
        else
            return null;

        
    }
}

