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
}
