using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Curve curve = new Curve();

    private void OnDrawGizmos()
    {
        curve.DrawGizmo(Color.cyan, transform.localToWorldMatrix);
    }
}
