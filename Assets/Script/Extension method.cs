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
        if (vector.x >= rect.xMin && vector.x <= rect.xMax)
            if (vector.y >= rect.yMin && vector.y <= rect.yMax)
                return true;
        return false;
    }
}

