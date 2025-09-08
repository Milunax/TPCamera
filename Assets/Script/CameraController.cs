using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Camera mainCamera;
    private CameraConfiguration configuration;

    private List<AView> activeViews;

    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        activeViews = new List<AView>();
    }

    private void Update()
    {
        ApplyConfiguration();

        configuration = ComputeAverage();
    }

    public void AddView(AView view)
    {
        activeViews.Add(view);
    }

    public void RemoveView(AView view)
    {
        activeViews.Remove(view);
    }

    private void ApplyConfiguration()
    {
        mainCamera.transform.position = configuration.GetPosition();
        mainCamera.transform.rotation = configuration.GetRotation();
    }

    private CameraConfiguration ComputeAverage()
    {
        float totalWeight = 0.0f;
        float averageYaw = ComputeAverageYaw();
        float totalPitch = 0.0f;
        float totalRoll = 0.0f;

        float totalFov = 0.0f;
        float totalDistance = 0.0f;
        Vector3 totalPivot = Vector3.zero;

        foreach(AView view in activeViews)
        {
            totalWeight += view.weight;

            totalPitch += view.GetConfiguration().pitch * view.weight;
            totalRoll += view.GetConfiguration().roll * view.weight;
            totalFov += view.GetConfiguration().fov * view.weight;
            totalDistance += view.GetConfiguration().distance * view.weight;
            totalPivot += view.GetConfiguration().pivot * view.weight;
        }

        CameraConfiguration newConfiguration = new CameraConfiguration(
                averageYaw,
                totalPitch / totalWeight,
                totalRoll / totalWeight,
                totalFov / totalWeight,
                totalDistance / totalWeight,
                totalPivot / totalWeight
            );

        return newConfiguration;
    }

    public float ComputeAverageYaw()
    {
        Vector2 sum = Vector2.zero;
        foreach (AView view in activeViews)
        {
            CameraConfiguration config = view.GetConfiguration();
            sum += new Vector2(Mathf.Cos(config.yaw * Mathf.Deg2Rad), Mathf.Sin(config.yaw * Mathf.Deg2Rad)) * view.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
    }

    private void OnDrawGizmos()
    {
        configuration.DrawGizmos(Color.red);
    }
}
