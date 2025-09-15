using UnityEngine;

public class FixedFollowView : AView
{
    public float roll = 90, fov = 90f, yawOffsetMax = 45f, pitchOffsetMax = 30f;
    public Transform target;
    public GameObject centralPoint;
    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration configuration = new CameraConfiguration();

        if (target != null)
        {
            float targetYaw, targetPitch;

            Vector3 targetDirection  = (target.position - transform.position).normalized;
            targetYaw = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
            targetPitch = -Mathf.Asin(targetDirection.y) * Mathf.Rad2Deg;

            if (centralPoint != null)
            {
                float centralYaw, centralPitch;
                Vector3 centralDirection = (centralPoint.transform.position - transform.position).normalized;
                centralYaw = Mathf.Atan2(centralDirection.x, centralDirection.z) * Mathf.Rad2Deg;
                centralPitch = Mathf.Asin(centralDirection.y) * Mathf.Rad2Deg;

                float deltaYaw = Mathf.DeltaAngle(centralYaw, targetYaw);
                float deltaPitch = Mathf.DeltaAngle(centralPitch, targetPitch);

                configuration.yaw = centralYaw + Mathf.Clamp(deltaYaw, - yawOffsetMax, yawOffsetMax);
                configuration.pitch = centralPitch + Mathf.Clamp(deltaPitch, - pitchOffsetMax, pitchOffsetMax);
            }
        }   
        configuration.roll = roll;
        configuration.fov = fov;
        configuration.distance = 0f;
        configuration.pivot = transform.position;

        return configuration;
    }
}
