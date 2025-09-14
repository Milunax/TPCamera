using UnityEngine;

public class Curve
{
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;

    public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A, B, C, D, t);
    }

    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        return localToWorldMatrix.MultiplyPoint(MathUtils.CubicBezier(A, B, C, D, t));
    }

    void DrawGizmo(Color c, Matrix4x4 localToWorldMatrix)
    {

    }
}
