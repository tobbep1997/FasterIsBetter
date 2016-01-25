using UnityEngine;
using System;

public static class Extension_method
{
    //--------------------------------------    Boolens
    public static int ToInt(this bool _bool)
    {
        if (_bool)
            return 1;
        else
            return 0;
    }
    //--------------------------------------    UnityEngien.Rect
    public static bool ContainsVector(this Rect rect, Vector2 vector)
    {
        if (vector.x >= rect.position.x && vector.x <= rect.position.x + rect.width)
            if (vector.y >= rect.position.y && vector.y <= rect.position.y + rect.height)
                return true;
        return false;
    }
    public static Vector2 ReturnScreenPosition(this Vector2 vector, Camera cam)
    {
        return cam.WorldToScreenPoint(vector);        
    }
}

