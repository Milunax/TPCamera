using UnityEngine;

public class DollyView : AView
{
    public float roll = 90f, distance, fov = 90f, distanceOnRail, speed;
    public bool isAuto = false;
    public Transform target;
    public Rail rail;

    public override CameraConfiguration GetConfiguration()
    {
        float targetYaw = 0f;
        float targetPitch = 0f;

        if (target != null)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            targetYaw = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
            targetPitch = Mathf.Asin(targetDirection.z) * Mathf.Rad2Deg;
        }

        CameraConfiguration cameraConfiguration = new CameraConfiguration
        {
            yaw = targetYaw,
            pitch = targetPitch,
            roll = roll,
            distance = distance,
            fov = fov,
        };
        return cameraConfiguration;
    }
}
