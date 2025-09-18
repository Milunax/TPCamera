using UnityEngine;

public abstract class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;

    public int Uid;

    public static int nextUid = 0;

    protected bool IsActive { get; private set; }

    private void Awake()
    {
        Uid = nextUid;
        nextUid++;
    }

    protected void SetActive(bool isActive)
    {
        if (isActive) ViewVolumeBlender.Instance.AddVolume(this);
        else if(!isActive) ViewVolumeBlender.Instance.RemoveVolume(this);
        IsActive = isActive;
    }

    public virtual float ComputeSelfWeight()
    {
        return 1.0f;
    } 
}
