using Unity.VisualScripting;
using UnityEngine;

public class DollyView : AView
{
    public float roll;
    public float distance;
    private Vector3 autoNearestPos;
    public float fov;
    public Transform target;
    public Rail rail;
    public float speed;
    public bool isAuto;

    private float distanceOnRail = 0.0f;

    public override CameraConfiguration GetConfiguration()
    {
        (float yaw, float pitch) = CalculateAngles(target.position);

        if(!isAuto) return new CameraConfiguration(yaw, pitch, roll, fov, 0, rail.GetPosition(distanceOnRail));

        return new CameraConfiguration(yaw, pitch, roll, fov, 0, autoNearestPos);
    }

    private (float, float) CalculateAngles(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.Normalize();
        float tempYaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float tempPitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;

        return (tempYaw, tempPitch);
    }

    private void Update()
    {
        if(!isAuto) distanceOnRail = Mathf.Clamp(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0.0f, rail.GetLength());
        else
        {
            int railChildIndex = 0;

            Vector3 nearestPosition = rail.transform.GetChild(0).position;

            while(railChildIndex + 1 < rail.transform.childCount)
            {
                Vector3 proj = MathUtils.GetNearestPointOnSegment(rail.transform.GetChild(railChildIndex).position, rail.transform.GetChild(railChildIndex + 1).position, target.position);

                float distBetweenTargetAndProj = Vector3.Distance(proj, target.position);

                if(Vector3.Distance(nearestPosition, target.position) > distBetweenTargetAndProj) nearestPosition = proj;
            }

            autoNearestPos = nearestPosition;
        }
    }
}
