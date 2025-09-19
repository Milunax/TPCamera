using UnityEngine;

public class DollyView : AView
{
    public float roll, distance, fov = 90f, distanceOnRail, speed = 1f;
    public bool isAuto = false;
    public Transform target;
    public Rail rail;

    void Update()
    {
        if (isAuto)
        {
            distanceOnRail = rail.GetDistanceFromPosition(target.position);
        }
        else
        {
            float input = Input.GetAxis("Horizontal");

            distanceOnRail += input * speed * Time.deltaTime;
        }
        if (!rail.isLoop)
        {
            distanceOnRail = Mathf.Clamp(distanceOnRail, 0f, rail.GetLength());
        }
    }
    public override CameraConfiguration GetConfiguration()
    {
        float targetYaw = 0, targetPitch = 0;
        Vector3 cameraPosition = rail.GetPosition(distanceOnRail);

        if (target != null)
        {
            Vector3 targetDirection = (target.position - cameraPosition).normalized;

            targetYaw = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
            targetPitch = -Mathf.Asin(targetDirection.y) * Mathf.Rad2Deg;
        }

        CameraConfiguration cameraConfiguration = new CameraConfiguration
        {
            yaw = targetYaw,
            pitch = targetPitch,
            roll = roll,
            fov = fov,
            pivot = cameraPosition,
            distance = 0f
        };
        return cameraConfiguration;
    }
}