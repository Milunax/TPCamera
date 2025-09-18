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
        return length;
    }

    public Vector3 GetPosition(float distance)
    {
        
        float distLeft = distance;
        int childIndex = 0;

        while (distance > 0)
        {
            float distBetweenCurrentAndNextChild = Vector3.Distance(transform.GetChild(childIndex).position, transform.GetChild(childIndex + 1).position);

            if (distLeft - distBetweenCurrentAndNextChild <= 0)
            {
                Vector3 dir = transform.GetChild(childIndex + 1).position - transform.GetChild(childIndex).position;
                dir.Normalize();

                return dir * distLeft;
            }
            else
            {
                distLeft -= distBetweenCurrentAndNextChild;
            }

            if (!isLoop)
            {
                if (childIndex + 1 == transform.childCount && distLeft >= 0)
                {
                    return transform.transform.GetChild(childIndex).position;
                }
                childIndex++;
            }
            else
            {
                if(childIndex + 1 == transform.childCount && distLeft > 0) childIndex = 0;
                else if(distLeft > 0) childIndex++;
            }        
        }

        //on trouve pas de point
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
