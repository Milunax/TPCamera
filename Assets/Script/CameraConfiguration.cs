using System;
using UnityEngine;

[Serializable]
public struct CameraConfiguration
{
    public float yaw, pitch, roll, distance, fov;
    public Vector3 pivot;

    public CameraConfiguration(float pYaw, float pPitch, float pRoll, float pFov, float pDistance, Vector3 pPivot)
    {
        yaw = pYaw;
        pitch = pPitch;
        roll = pRoll;
        distance = pDistance;
        fov = pFov;
        pivot = pPivot;
    }

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(pitch, yaw, roll);
    }

    public Vector3 GetPosition()
    {
        return pivot + distance * Vector3.back;
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