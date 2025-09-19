using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

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
        if(active) CameraController.instance.AddView(this);
        else CameraController.instance.RemoveView(this);

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
