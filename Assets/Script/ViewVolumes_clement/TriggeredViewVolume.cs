using UnityEngine;

public class TriggeredViewVolume : AViewVolume
{
    [SerializeField] private GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        //Verification a revoir
        if(target.TryGetComponent(out Collider collider) && collider == other)
        {
            SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (target.TryGetComponent(out Collider collider) && collider == other)
        {
            SetActive(false);
        }
    }
}
