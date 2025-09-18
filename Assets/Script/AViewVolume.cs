using UnityEngine;

public abstract class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;

    private int Uid;

    public static int nextUid = 0;

    public virtual float ComputeSelfWeight()
    {
        return 1.0f;
    }

    private void Awake()
    {
        Uid = nextUid;
        nextUid++;
    }
}
