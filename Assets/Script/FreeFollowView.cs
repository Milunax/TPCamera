using UnityEditor.Profiling;
using UnityEngine;

public class FreeFollowView : AView
{
    [SerializeField] float[] pitch = new float[3];
    [SerializeField] float[] roll = new float[3];
    [SerializeField] float[] fov = new float[3];

    [SerializeField] float yaw;
    [SerializeField] float yawSpeed;

    [SerializeField] Transform target;

    [SerializeField] Curve curve = new Curve();
    [SerializeField] float curvePosition;
    [SerializeField] float curveSpeed;

    public override CameraConfiguration GetConfiguration()
    {
        //(float yaw, float pitch) = CalculateAngles(target.position);

        return new CameraConfiguration(
            yaw,
            MathUtils.QuadraticBezier(pitch[0], pitch[1], pitch[2], curvePosition),
            MathUtils.QuadraticBezier(roll[0], roll[1], roll[2], curvePosition),
            MathUtils.QuadraticBezier(fov[0], fov[1], fov[2], curvePosition),
            0,
            curve.GetPosition(curvePosition)
            );
        //return new CameraConfiguration(yaw, pitch, roll, fov, 0, transform.position);
    }

    private void Update()
    {
        yaw = Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

        curvePosition = Input.GetAxis("Vertical") * curveSpeed * Time.deltaTime;
    }

    /*private CameraConfiguration GetTopConfiguration()
    {
        return new CameraConfiguration(
            yaw,
            pitch[0],
            roll[0],
            fov[0],
            0,
            transform.position // A CHANGER
            );
    }

    private CameraConfiguration GetMiddleConfiguration()
    {
        return new CameraConfiguration(
            yaw,
            pitch[1],
            roll[1],
            fov[1],
            0,
            transform.position // A CHANGER
            );
    }

    private CameraConfiguration GetBottomConfiguration()
    {
        return new CameraConfiguration(
            yaw,
            pitch[2],
            roll[2],
            fov[2],
            0,
            transform.position // A CHANGER
            );
    }*/

    private (float, float) CalculateAngles(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.Normalize();
        float tempYaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float tempPitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;

        return (tempYaw, tempPitch);
    }

    private void OnDrawGizmos()
    {
        curve.DrawGizmo(Color.green, transform.localToWorldMatrix);
    }
}
