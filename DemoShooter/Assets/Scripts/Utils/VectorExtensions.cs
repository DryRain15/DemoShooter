using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 GetXY0(this Vector3 v, float z = 0f)
    {
        return new Vector3(v.x, v.y, z);
    }
}
