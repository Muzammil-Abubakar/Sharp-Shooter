using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private LayerMask raycastLayers;

    public void Shoot(WeaponSO weaponData)
    {
        muzzle.Play();

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
                enemyHealth.TakeDamage(weaponData.Damage);
        }
    }
}