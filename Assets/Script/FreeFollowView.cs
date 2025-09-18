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
        Matrix4x4 curveToWorldMatrix = Matrix4x4.TRS(target.position, Quaternion.Euler(0, yaw, 0), Vector3.one);

        return new CameraConfiguration(
            yaw,
            MathUtils.QuadraticBezier(pitch[0], pitch[1], pitch[2], curvePosition),
            MathUtils.QuadraticBezier(roll[0], roll[1], roll[2], curvePosition),
            MathUtils.QuadraticBezier(fov[0], fov[1], fov[2], curvePosition),
            0,
            curve.GetPosition(curvePosition, curveToWorldMatrix)
            );
    }

    private void Update()
    {
        yaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

        curvePosition = Mathf.Clamp(curvePosition + Input.GetAxis("Vertical") * curveSpeed * Time.deltaTime, 0.0f, 1.0f);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        for(int i = 0; i < 3; i++)
        {
            new CameraConfiguration(yaw, pitch[i], roll[i], fov[i], 0, curve.GetPosition(i * 0.5f, Matrix4x4.TRS(target.position, Quaternion.Euler(0, yaw, 0), Vector3.one))).DrawGizmos(Color.green);
        }

        curve.DrawGizmo(Color.green, transform.localToWorldMatrix);
    }
}
