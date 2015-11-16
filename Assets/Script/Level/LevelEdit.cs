using UnityEngine;
using System.Collections;

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
    private Collider2D CurrentCollider;

    private void Start()
    {
        CurrentCollider = GetComponent<Collider2D>();
        
    }
	void Update ()
    {
        SetPosition();

        if (CheckIfObjectsAllAround())
        {
            
        }
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
        if (CurrentCollider.isTrigger)
            return true;

        Ray2D ray;
        RaycastHit2D[] hits;
        for (float i = 0; i <= Mathf.PI * 2; i += Mathf.PI/2)
        {
            ray = new Ray2D(CurrentCollider.bounds.center, new Vector2(Mathf.Cos(i), Mathf.Sin(i)));
            hits = Physics2D.RaycastAll(CurrentCollider.bounds.center, ray.direction, CurrentCollider.bounds.extents.x * 2);

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
                Debug.DrawRay(CurrentCollider.bounds.center, ray.direction * Mathf.Sqrt(CurrentCollider.bounds.extents.sqrMagnitude), Color.green);
                //return false;
            }
        }
        return true;
    }
    public bool CheckIfObjectAtSamePlace()
    {
        Ray2D ray = new Ray2D(CurrentCollider.bounds.center,Vector2.zero);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin,ray.direction,CurrentCollider.bounds.extents.x);



        return true;
    }
}
