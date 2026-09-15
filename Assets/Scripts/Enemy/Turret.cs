using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Component target;
    [SerializeField] private Transform turretHead;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float fireRate = 2f;

    private float fireTimer;

    private void Update()
    {
        HandleLookAt();
        HandleFiring();
    }

    private void HandleLookAt()
    {
        if (target == null || turretHead == null)
            return;

        turretHead.LookAt(target.transform);
    }

    private void HandleFiring()
    {
        if (target == null || projectilePrefab == null || projectileSpawnPoint == null)
            return;

        fireTimer += Time.deltaTime;

        float fireInterval = 1f / fireRate;

        if (fireTimer >= fireInterval)
        {
            Instantiate(
                projectilePrefab,
                projectileSpawnPoint.position,
                projectileSpawnPoint.rotation
            );

            fireTimer = 0f;
        }
    }
}