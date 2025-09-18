using UnityEngine;

public class TriggeredViewVolume : AViewVolume
{
    [SerializeField] private GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        {
            SetActive(true);          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            SetActive(false);
        }
    }
}
