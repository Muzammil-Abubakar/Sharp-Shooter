using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponData;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitVFX;

    private Camera mainCamera;
    private StarterAssets.StarterAssetsInputs input;
    private float nextFireTime;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        mainCamera = Camera.main;
        input = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
    }

    void Update()
    {
        if (input.shoot && Time.time >= nextFireTime)
        {
            Shoot();

            nextFireTime = Time.time + weaponData.FireRate;

            input.ShootInput(false);
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        animator.Play(SHOOT_STRING, 0, 0f);

        Ray ray = mainCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Ray is hitting: " + hit.collider.gameObject.name);

            Instantiate(
                hitVFX,
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