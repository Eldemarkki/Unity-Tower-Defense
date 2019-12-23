using UnityEngine;

public static class Utils
{
    public static float SqrDistance(Vector2 a, Vector2 b)
    {
        return (a - b).sqrMagnitude;
    }
}
