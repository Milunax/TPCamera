using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight = 1.0f;
    public bool isActiveOnStart = false;
    public abstract CameraConfiguration GetConfiguration();
    public void Start()
    {
        if (isActiveOnStart)
            SetActive();
    }
    public void SetActive()
    {
        CameraController.instance.AddView(this);
    }
    public void OnDisable()
    {
        CameraController.instance.RemoveView(this);
    }

}
