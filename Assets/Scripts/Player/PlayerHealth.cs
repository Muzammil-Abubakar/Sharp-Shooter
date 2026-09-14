using UnityEngine;
using Unity.Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 5;

    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private Transform weaponCamera;

    [SerializeField] private int deathCameraPriority = 20;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log(damage + " damage taken. Health remaining: " + health);

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        weaponCamera.SetParent(null);
        virtualCamera.Priority = deathCameraPriority;

        Destroy(gameObject);
    }
}