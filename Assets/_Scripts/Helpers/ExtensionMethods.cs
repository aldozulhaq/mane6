using JetBrains.Annotations;
using UnityEngine;

public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static Vector3 WithX(this Vector3 vector, float x)
    {
        Debug.Log("before: " + vector);
        Debug.Log("after: " + x);
        return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 WithY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 WithZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector2 WithX(this Vector2 vector, float x)
    {
        return new Vector2(x, vector.y);
    }

    public static Vector2 WithY(this Vector2 vector, float y)
    {
        return new Vector2(vector.x, y);
    }

    public static Vector3 AddX(this Vector3 vector, float x)
    {
        return new Vector3(vector.x + x, vector.y, vector.z);
    }

    public static Vector3 AddY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, vector.y + y, vector.z);
    }

    public static Vector3 AddZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, vector.z + z);
    }
}
