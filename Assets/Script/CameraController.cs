using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Camera mainCamera;
    private CameraConfiguration configuration;
    private List<AView> activeViews = new List<AView>();

    private void Awake()
    {
        if(instance && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        configuration = ComputeAverage();
        ApplyConfiguration(mainCamera);
    }

    private void ApplyConfiguration(Camera camera)
    {
        camera.transform.position = configuration.GetPosition();
        camera.transform.rotation = configuration.GetRotation();
        camera.fieldOfView = configuration.fov;
    }
    public void AddView(AView view)
    {
        if (!activeViews.Contains(view))
            activeViews.Add(view);
    }
    public void RemoveView(AView view)
    {
        if (activeViews.Contains(view))
            activeViews.Remove(view);
    }
    public CameraConfiguration ComputeAverage()
    {
        float totalWeight = activeViews.Sum(view => view.weight);
        float sumPitch = 0f, sumRoll = 0f, sumFOV = 0f,sumDistance = 0f;
        Vector3 sumPivot = Vector3.zero;
        
        foreach (AView view in activeViews)
        {
            CameraConfiguration config = view.GetConfiguration();
            float w = view.weight / totalWeight;
            sumPitch += config.pitch * w;
            sumRoll += config.roll * w;
            sumFOV += config.fov * w;
            sumDistance += config.distance * w;
            sumPivot += config.pivot * w;
        }
        return new CameraConfiguration
        {
            yaw = ComputeAverageYaw(),
            pitch = sumPitch,
            roll = sumRoll,
            fov = sumFOV,
            distance = sumDistance,
            pivot = sumPivot,
        };
    }
    public float ComputeAverageYaw()
    {
        Vector2 sum = Vector2.zero;
        foreach (AView view in activeViews)
        {
            CameraConfiguration config = view.GetConfiguration();
            sum += new Vector2(Mathf.Cos(config.yaw * Mathf.Deg2Rad),
            Mathf.Sin(config.yaw * Mathf.Deg2Rad)) * view.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
    }

    private void OnDrawGizmos()
    {
        configuration.DrawGizmos(Color.red);
    }
}