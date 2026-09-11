using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform target;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.SelfDestruct();
            }
        }
    }
}