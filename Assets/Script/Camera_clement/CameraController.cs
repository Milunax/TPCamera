using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Camera mainCamera;

    [SerializeField] private float speed;
    private CameraConfiguration configuration;

    private List<AView> activeViews = new List<AView>();

    private CameraConfiguration currentConfiguration;
    private CameraConfiguration targetConfiguration;

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
        currentConfiguration = ComputeAverage();
        targetConfiguration = ComputeAverage();
    }

    private void Update()
    {
        ApplyConfiguration();
        
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
        targetConfiguration = ComputeAverage();

        if(speed * Time.deltaTime < 1.0f)
        {
            Vector2 currentYawDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * currentConfiguration.yaw), Mathf.Sin(Mathf.Deg2Rad * currentConfiguration.yaw));
            Vector2 targetYawDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * targetConfiguration.yaw), Mathf.Sin(Mathf.Deg2Rad * targetConfiguration.yaw));


            currentConfiguration.pivot = Vector3.Lerp(currentConfiguration.pivot, targetConfiguration.pivot, speed * Time.deltaTime);
            currentConfiguration.distance = Mathf.Lerp(currentConfiguration.distance, targetConfiguration.distance, speed * Time.deltaTime);
            currentConfiguration.pitch = Mathf.Lerp(currentConfiguration.pitch, targetConfiguration.pitch, speed * Time.deltaTime);
            currentConfiguration.yaw = Vector2.SignedAngle(Vector2.right, (Vector2.Lerp(currentYawDir, targetYawDir, speed * Time.deltaTime)));
            currentConfiguration.roll = Mathf.Lerp(currentConfiguration.roll, targetConfiguration.roll, speed * Time.deltaTime);


            mainCamera.transform.position = currentConfiguration.GetPosition();
            mainCamera.transform.rotation = currentConfiguration.GetRotation();
        }  
        else
        {
            currentConfiguration = targetConfiguration;
            mainCamera.transform.position = targetConfiguration.GetPosition();
            mainCamera.transform.rotation = targetConfiguration.GetRotation();
        }
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
        if (Mathf.Approximately(totalWeight, 0.0f)) totalWeight = 1;

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
