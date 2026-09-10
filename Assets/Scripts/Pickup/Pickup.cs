using UnityEngine;

public class Pickup : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickup(other);
        }
    }

    protected virtual void OnPickup(Collider other)
    {
    }
}