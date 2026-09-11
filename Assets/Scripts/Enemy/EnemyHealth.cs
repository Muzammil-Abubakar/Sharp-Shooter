using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private GameObject robotExplosion;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Enemy took " + damage + " damage. Health remaining: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void SelfDestruct()
    {
        Debug.Log("Enemy self-destructed!");

        // Spawn the explosion VFX at the enemy's position.
        if (robotExplosion != null)
        {
            Instantiate(
                robotExplosion,
                transform.position,
                transform.rotation
            );
        }

        Destroy(gameObject);
    }

    private void Die()
    {
        Debug.Log("Enemy died!");

        // Spawn the explosion VFX at the enemy's position.
        if (robotExplosion != null)
        {
            Instantiate(
                robotExplosion,
                transform.position,
                transform.rotation
            );
        }

        Destroy(gameObject);
    }
}