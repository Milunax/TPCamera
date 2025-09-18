using UnityEngine;

public static class MathUtils
{
    public static Vector3 GetNearestPointOnSegment(Vector3 firstNode, Vector3 secondNode, Vector3 target)
    {
        Vector3 AB = secondNode - firstNode;
        Vector3 AC = target - firstNode;

        float t = Vector3.Dot(AC, AB) / Vector3.Dot(AB, AB); // param�tre de projection

        t = Mathf.Clamp01(t); // tr�s important : on limite � [0,1] pour rester sur le segment

        return firstNode + t * AB;
    }
}