using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop = false;
    private float length;
    private readonly Transform[] nodes;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            nodes[child.GetSiblingIndex()] = child;
        }
    }
    public void Start()
    {
        foreach(Transform node in nodes)
        {
            if (nodes[node.GetSiblingIndex() + 1] != null)
            length += Vector3.Distance(node.position, nodes[node.GetSiblingIndex() + 1].position);
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
        Transform currentNode = nodes[index % nodes.Length];
        Transform nextNode = nodes[index + 1 % nodes.Length];

        Vector3 targetPosition = new Vector3();

        while (distance > 0)
        {
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            if (nodes[i + 1] != null)
            {
                Gizmos.DrawLine(nodes[i].position, nodes[i + 1].position);
            }
            if (isLoop)
            {
                Gizmos.DrawLine(nodes[^1].position, nodes[0].position);
            }
        }
    }
}
