using UnityEngine;

public class FixedFollowView : AView
{
    public float roll;
    public float fov;

    public Transform target;
    public Transform centralPoint;
    public float yawOffsetMax;
    public float pitchOffsetMax;

    private float yaw;
    private float pitch;

    public override CameraConfiguration GetConfiguration()
    {
        (yaw, pitch) = CalculateAngles(target.position);

        (float centralYaw, float centralPitch) = CalculateAngles (centralPoint.position);

        float yawDiff = Mathf.DeltaAngle(centralPitch, yaw);
        float pitchDiff = Mathf.DeltaAngle(centralPitch, pitch);

        return new CameraConfiguration(centralYaw + Mathf.Clamp(yaw, -yawOffsetMax, yawOffsetMax), centralPitch + Mathf.Clamp(pitch, -pitchOffsetMax, pitchOffsetMax), roll, fov, 0, transform.position);
    }

    private (float, float) CalculateAngles(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.Normalize();
        float tempYaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float tempPitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;

        return (tempYaw, tempPitch);
    }
}
