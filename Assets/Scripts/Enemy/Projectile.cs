using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject projectileHitVFX;

    private Rigidbody rigidBody;
    private int damage;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Init(int damage)
    {
        this.damage = damage;
    }

    private void Start()
    {
        rigidBody.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {

        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }


        Instantiate(projectileHitVFX, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}