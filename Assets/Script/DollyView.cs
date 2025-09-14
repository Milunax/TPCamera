using UnityEngine;

public class DollyView : AView
{
    public float roll;
    public float distance;
    public float fov;

    public Transform target;

    public Rail rail;
    public float distanceOnRail = 0.0f;
    public float speed;

    public override CameraConfiguration GetConfiguration()
    {
        (float yaw, float pitch) = CalculateAngles(target.position);

        return new CameraConfiguration(yaw, pitch, roll, fov, 0, rail.GetPosition(distanceOnRail));
    }

    private (float, float) CalculateAngles(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.Normalize();
        float tempYaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float tempPitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;

        return (tempYaw, tempPitch);
    }

    private void FixedUpdate() //Maybe a l'update ??
    {
        distanceOnRail = Mathf.Clamp(Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime, 0.0f, rail.GetLength());
    }
}
