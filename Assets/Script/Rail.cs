using Unity.VisualScripting;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop;

    private float length;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(i + 1 <= transform.childCount) length += Vector3.Distance(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            else if(i + 1 > transform.childCount && isLoop) length += Vector3.Distance(transform.GetChild(i).position, transform.GetChild(0).position);
        }
    }

    public float GetLength()
    {
        return Vector3.Distance(transform.GetChild(0).position, transform.GetChild(transform.childCount - 1).position);
    }

    public Vector3 GetPosition()
    {
        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(Color.yellow);
    }

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i + 1 < transform.childCount) Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            else if (i + 1 >= transform.childCount && isLoop) Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(0).position);
        }
    }
}
