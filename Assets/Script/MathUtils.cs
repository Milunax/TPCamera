using UnityEngine;

public static class MathUtils 
{
    public static Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 ABNorm = (b - a).normalized;

        float dotProduct = Mathf.Clamp(Vector3.Dot(target - a, ABNorm), 0, Vector3.Distance(a, b));

        Vector3 projTarget = a + ABNorm * dotProduct;

        return projTarget;
    }

    public static Vector3 LinearBezier(Vector3 A, Vector3 B, float t)
    {
        return (1 -  t) * A + t * B;
    }

    public static Vector3 QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t)
    {
        return (1 -t)* LinearBezier(A, B, t) + t * LinearBezier(B, C, t);
    }

    public static Vector3 CubicBezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t)
    {
        return (1 - t) * QuadraticBezier(A, B, C, t) + t * QuadraticBezier(B, C, D, t);
    }

    public static float LinearBezier(float A, float B, float t)
    {
        return (1 - t) * A + t * B;
    }

    public static float QuadraticBezier(float A, float B, float C, float t)
    {
        return (1 - t) * LinearBezier(A, B, t) + t * LinearBezier(B, C, t);
    }

    public static float CubicBezier(float A, float B, float C, float D, float t)
    {
        return (1 - t) * QuadraticBezier(A, B, C, t) + t * QuadraticBezier(B, C, D, t);
    }

}
