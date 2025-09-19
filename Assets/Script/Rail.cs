using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class Rail : MonoBehaviour
{
    public bool isLoop = false;
    private float length;
    private Transform[] nodes;

    private int childCount;
    private Transform currentNode;
    private Transform nextNode;

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
        childCount = transform.childCount;

        Vector3 targetPosition = transform.GetChild(0).position;

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
    public float GetDistanceFromPosition(Vector3 targetPosition)
    {
        childCount = transform.childCount;

        Transform node = transform.GetChild(0);
        float segmentDistance = 0;

        float shortestDistance = float.PositiveInfinity;

        for (int i = 0; i < childCount; i++)
        {
            currentNode = transform.GetChild(i % childCount);
            nextNode = transform.GetChild((i + 1) % childCount);

            if (currentNode != null && nextNode != null)
            {
                Vector3 nearestPointOnSegment = MathUtils.GetNearestPointOnSegment(currentNode.position, nextNode.position, targetPosition);
                float targetDistance = Vector3.Distance(nearestPointOnSegment, targetPosition);

                if (targetDistance < shortestDistance)
                {
                    shortestDistance = targetDistance;
                    node = currentNode;
                    segmentDistance = Vector3.Distance(currentNode.position, nearestPointOnSegment);
                }
            }
        }
        if (node != null)
            return GetDistanceFromPoint(node) + segmentDistance;
        return 0;
    }
    public float GetDistanceFromPoint(Transform point)
    {
        float distance = 0;
        childCount = transform.childCount;

        if (!point.IsChildOf(transform)) return distance;

        for (int i  = 0; i < transform.childCount;i++)
        {
            currentNode = transform.GetChild(i % childCount);
            nextNode = transform.GetChild((i + 1) % childCount);

            if (currentNode == point)
            {
                return distance;
            }
            distance += Vector3.Distance(currentNode.position, nextNode.position);
        }
        return distance;
    }
    void OnDrawGizmos()
    {
        if (transform.childCount < 2) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            currentNode = transform.GetChild(i);
            nextNode = transform.GetChild(i + 1);

            if (currentNode != null && nextNode != null)
            {
                Gizmos.DrawLine(currentNode.position, nextNode.position);
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
