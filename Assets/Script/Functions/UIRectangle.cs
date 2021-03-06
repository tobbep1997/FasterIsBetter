﻿using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class UIRectangle {

    [SerializeField]
    private Vector2 _Position;
    [SerializeField]
    private float _Width, _Hight, _xMax, _yMax;    

    public Vector2 Position
    {
        get { return _Position; }
        set { _Position = value; }
    }
    public float Width
    {
        get { return _Width; }
        set { _Width = CheckIfNull<float>(value) ?? _Width; }
    }
    public float Hight
    {
        get { return _Hight; }
        set { _Hight = CheckIfNull<float>(value) ?? _Hight; }
    }
    public float xMax
    {
        get { return _xMax; }
    }
    public float yMax
    {
        get { return _yMax; }
    }

    //Checks if current T is null and returns its defult value if the current T is null
    private T? CheckIfNull<T>(T type) where     
        T : struct                              
    {
        if (type.Equals(null))        
            return default(T);        
        return type;
    }
    //--------------------------------------------  Construcors
    public UIRectangle()
    {
        _Position = Vector2.zero;
        _Width = 0;
        _Hight = 0;
        SetMaxValues();
    }
    public UIRectangle(Vector2 Position)
    {
        _Position = Position;
        _Width = 0;
        _Hight = 0;
        SetMaxValues();
    }
    public UIRectangle(Vector2 Position, float _Width, float _Hight)
    {
        _Position = Position;
        this._Width = _Width;
        this._Hight = _Hight;
        SetMaxValues();
    }
    private void SetMaxValues()
    {
        _xMax = _Position.x + _Width;
        _yMax = _Position.y + _Hight;
    }
    //--------------------------------------------  functions
    public bool ContainsVector(Vector2 vector)  //Returns true if the current vector is inside of the rectangle
    {
        return vector.x >= _Position.x && vector.x <= _xMax && vector.y >= _Position.y && vector.y <= _yMax;
    } 
}
