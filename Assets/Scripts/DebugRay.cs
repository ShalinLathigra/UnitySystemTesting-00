using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DebugRay
{
    public Vector2 origin { get; }
    public Vector2 direction { get; }
    public Color color { get; }


    public DebugRay(Vector3 _origin, Vector3 _direction)
    {
        origin = _origin;
        direction = _direction;
        color = Color.red;
    }
    
    public DebugRay(Vector3 _origin, Vector3 _direction, Color _color)
    {
        origin = _origin;
        direction = _direction;
        color = _color;
    }

}
