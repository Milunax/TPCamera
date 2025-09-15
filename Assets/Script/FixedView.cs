using UnityEngine;

public class FixedView : AView
{
    public float yaw, pitch, roll, fov = 90f;
    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration cameraConfiguration = new CameraConfiguration
        {
            yaw = yaw,
            pitch = pitch,
            roll = roll,
            fov = fov,
            pivot = transform.position,
            distance = 0f
        };
        return cameraConfiguration;
    }
}
