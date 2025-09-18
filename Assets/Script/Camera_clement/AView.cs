using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;
    public bool isCutOnSwitch;
    //public bool isActiveOnStart;

    /* private void Start()
    {
        if (isActiveOnStart) SetActive(true);
    }*/

    public abstract CameraConfiguration GetConfiguration();
    public void SetActive(bool active)
    {
        CameraController.instance.AddView(this);
        if (isCutOnSwitch)
        {
            ViewVolumeBlender.Instance.Update();
            CameraController.instance.Cut();
        }
    }

    public virtual void OnDrawGizmos()
    {
        GetConfiguration().DrawGizmos(Color.green);
    }
}
