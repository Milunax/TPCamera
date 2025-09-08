using UnityEngine;

public class FixedView : AView
{
    public float yaw, pitch, roll, fov;

    public override CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(yaw, pitch, roll, fov, 0, transform.position);
    }
}
