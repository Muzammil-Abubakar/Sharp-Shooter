using UnityEngine;
using Unity.Cinemachine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private LayerMask raycastLayers;

    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();

        if (impulseSource == null)
        {
            Debug.LogWarning(
                $"No CinemachineImpulseSource found on '{gameObject.name}'."
            );
        }
    }

    public void Shoot(WeaponSO weaponData)
    {
        // Play muzzle flash.
        muzzle.Play();

        // Fire camera impulse.
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayers))
        {
            Instantiate(
                weaponData.HitVFX,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(weaponData.Damage);
            }
        }
    }
}