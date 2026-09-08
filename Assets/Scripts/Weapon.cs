using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;

    private Camera mainCamera;
    private StarterAssets.StarterAssetsInputs input;

    void Awake()
    {
        mainCamera = Camera.main;
        input = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
    }

    void Update()
    {
        if (input.shoot)
        {
            // Play muzzle flash
            muzzleFlash.Play();

            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Ray is hitting: " + hit.collider.gameObject.name);

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