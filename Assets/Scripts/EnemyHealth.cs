using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Enemy took " + damage + " damage. Health remaining: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");

        Destroy(gameObject);
    }
}