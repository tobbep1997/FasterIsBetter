using UnityEngine;

[ExecuteInEditMode]
public class DrawButton : MonoBehaviour {

    public Texture2D Texture;
    public Vector2 Position;
    public float Scale = 1;


    void OnGUI()
    {
        if (!Texture)
            return;
        GUI.DrawTexture(new Rect(Position.x, Position.y, Texture.width * Scale, Texture.height * Scale), Texture, ScaleMode.ScaleToFit, true);
    }
}

