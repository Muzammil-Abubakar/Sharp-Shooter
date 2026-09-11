using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 5;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log(damage + " damage taken. Health remaining: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}