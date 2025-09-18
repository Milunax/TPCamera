using UnityEngine;

public class SphereViewVolume : AViewVolume
{
    public Transform target;
    public float innerRadius;
    public float outerRadius;

    private float distance;

    private void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);

        if(distance <= outerRadius && !IsActive) SetActive(true);
        else if(distance > outerRadius && IsActive) SetActive(false);
    }

    public override float ComputeSelfWeight()
    {
        return Mathf.Clamp(innerRadius/outerRadius, 0.0f, 1.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, innerRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, outerRadius);
    }

    private void OnValidate()
    {
        if(innerRadius > outerRadius)
        {
            innerRadius = outerRadius;
            Debug.LogError("The inner radius can not be greater than the outer radius : " + gameObject.name);
        }
    }
}
