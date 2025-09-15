using System;
using UnityEngine;

[Serializable]
public struct CameraConfiguration
{
    public float yaw, pitch, roll, distance, fov;
    public Vector3 pivot;

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(pitch, yaw, roll);
    }

    public Vector3 GetPosition()
    {
        Vector3 offset = GetRotation() * (Vector3.back * distance);
        return pivot + offset;
    }
    public static CameraConfiguration Lerp(CameraConfiguration startConfig, CameraConfiguration endConfig, float t)
    {
        return new CameraConfiguration
        {
            yaw = Mathf.Lerp(startConfig.yaw, endConfig.yaw, t),
            pitch = Mathf.Lerp(startConfig.pitch, endConfig.pitch, t),
            roll = Mathf.Lerp(startConfig.roll, endConfig.roll, t),
            fov = Mathf.Lerp(startConfig.fov, endConfig.fov, t),
            distance = Mathf.Lerp(startConfig.distance, endConfig.distance, t),
            pivot = Vector3.Lerp(startConfig.pivot, endConfig.pivot, t)
        };
    }

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fov, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
}