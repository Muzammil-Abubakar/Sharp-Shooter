using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health = 3;

    public void TakeDamage()
    {
        health--;

        Debug.Log("Enemy took damage. Health remaining: " + health);

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