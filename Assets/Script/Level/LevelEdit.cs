using UnityEngine;

[ExecuteInEditMode]//This makes sure that the script is always running
public class LevelEdit : MonoBehaviour {	
    /// <summary>
    /// This makes it easier to make levels and makes sure that everything is connected to a grid
    /// </summary>
	public Vector2 vCell_size;                  //this is the size of the tile that this script is on
	//public Vector2 vCell_Offset;

	private Vector2 vCurrentPos,vPreviousPos;   //This is the current and the previous position of the tile
	void Update () {

		vCurrentPos = transform.position;
        //This makes sure the tile is moved it will snap to the grid apropiet to the size of the tile
		if (vCurrentPos != vPreviousPos) {
			transform.position = new Vector3((Mathf.Round(transform.position.x / (vCell_size.x)) * (vCell_size.x)),
			                                 (Mathf.Round(transform.position.y / (vCell_size.y)) * (vCell_size.y)),
			                                 transform.position.z);
		}
		vPreviousPos = vCurrentPos;

	}
}
