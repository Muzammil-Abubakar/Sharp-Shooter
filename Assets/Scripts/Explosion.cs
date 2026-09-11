using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float radius = 1.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}