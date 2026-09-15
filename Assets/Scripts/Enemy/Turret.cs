using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Component target;
    [SerializeField] private Transform turretHead;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField, Range(1, 5)] private int damage = 2;

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
            FireProjectile();
            fireTimer = 0f;
        }
    }

    private void FireProjectile()
    {
        GameObject projectileObject = Instantiate(
            projectilePrefab,
            projectileSpawnPoint.position,
            projectileSpawnPoint.rotation
        );

        projectileObject.transform.LookAt(target.transform);

        Projectile projectile = projectileObject.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Init(damage);
        }
    }
}