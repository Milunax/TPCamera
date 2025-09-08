using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Camera camera;
    public CameraConfiguration configuration;

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

    private void Update()
    {
        ApplyConfiguration();
        
        
    }

    private void ApplyConfiguration()
    {
        camera.transform.position = configuration.GetPosition();
        camera.transform.rotation = configuration.GetRotation();
    }

    private void OnDrawGizmos()
    {
        configuration.DrawGizmos(Color.red);
    }
}
