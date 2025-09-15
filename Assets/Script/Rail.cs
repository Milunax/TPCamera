using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop = false;
    public float lenght;
    private Transform[] nodes;

    private void Awake()
    {
        nodes = new Transform[transform.childCount];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = transform.GetChild(i);
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        if (isLoop)
        {
            Gizmos.DrawLine(transform.GetChild(transform.childCount-1).position, transform.GetChild(0).position);
        }
    }
}
