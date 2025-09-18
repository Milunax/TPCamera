using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop = false;
    private float length;
    private Transform[] nodes;

    private void Awake()
    {
        nodes = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            nodes[i] = transform.GetChild(i);
        }
    }
    public void Start()
    {
        length = 0f;
        for ( int i = 0; i < nodes.Length -1 ; i++)
        {
            if (nodes[i] != null && nodes[i + 1] != null)
            {
                length += Vector3.Distance(nodes[i].position, nodes[i + 1].position);
            }
        }
        if (isLoop)
        {
            length += Vector3.Distance(nodes[^1].position, nodes[0].position);
        }
    }
    public float GetLength()
    {
        return length;
    }
    public Vector3 GetPosition(float distance)
    {
        int index = 0;
        int childCount = transform.childCount;

        Transform currentNode = transform.GetChild(index % childCount);
        Transform nextNode = transform.GetChild((index + 1) % childCount);

        Vector3 targetPosition = currentNode.position;

        while (distance > 0)
        {
            currentNode = transform.GetChild(index % childCount);
            nextNode = transform.GetChild((index + 1) % childCount);

            float nextDistance = Vector3.Distance(currentNode.position, nextNode.position);

            if (distance <= nextDistance)
            {
                float t = Mathf.InverseLerp(0, nextDistance, distance);
                targetPosition = Vector3.Lerp(currentNode.position, nextNode.position, t);
                return targetPosition;
            }
            distance -= nextDistance;
            index++;
        }
        return targetPosition;
    }
    public float GetNearestDistance(Vector3 position)
    {
        int index = 0;
        int childCount = transform.childCount;
        float distance = 0;

        Transform currentNode = transform.GetChild(index % childCount);
        Transform nextNode = transform.GetChild(( index + 1) % childCount);

        for (int i = 0; i < childCount; i++)
        {

        }

        return distance;
    }

    void OnDrawGizmos()
    {
        if (transform.childCount < 2) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Transform currentNode = transform.GetChild(i);
            Transform NextNode = transform.GetChild(i + 1);

            if (currentNode != null && NextNode != null)
            {
                Gizmos.DrawLine(currentNode.position, NextNode.position);
            }
        }
        if (isLoop)
        {
            Transform LastNode = transform.GetChild(transform.childCount - 1);
            Transform FirstNode = transform.GetChild(0);
            Gizmos.DrawLine(LastNode.position, FirstNode.position);
        }
    }
}
