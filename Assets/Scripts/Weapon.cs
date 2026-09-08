using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private ParticleSystem muzzleFlash;

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
            animator.Play(SHOOT_STRING,0,0f);
            input.ShootInput(false);

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