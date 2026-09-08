
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitVFX;

    private Camera mainCamera;
    private StarterAssets.StarterAssetsInputs input;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        mainCamera = Camera.main;
        input = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
    }

    void Update()
    {
        if (input.shoot)
        {
            muzzleFlash.Play();
            animator.Play(SHOOT_STRING, 0, 0f);

            Ray ray = mainCamera.ViewportPointToRay(
                new Vector3(0.5f, 0.5f, 0f)
            );

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Ray is hitting: " + hit.collider.gameObject.name);

                // Spawn hit VFX at the point where the ray hits
                Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));

                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage();
                }
            }

            input.ShootInput(false);
        }
    }
}

