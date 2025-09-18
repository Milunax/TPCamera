using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;

[Serializable]
public class Curve
{
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;

    public int curveSamples;

    public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A, B, C, D, t);
    }

    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        return localToWorldMatrix.MultiplyPoint(GetPosition(t));
    }

    public void DrawGizmo(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = c;

        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(A), 0.25f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(B), 0.25f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(C), 0.25f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(D), 0.25f);


        for (int i = 0; i < curveSamples; i++)
        {
            Gizmos.DrawLine(GetPosition((float)i / curveSamples, localToWorldMatrix), GetPosition((float)(i + 1) / curveSamples, localToWorldMatrix));
        }
    }
}
