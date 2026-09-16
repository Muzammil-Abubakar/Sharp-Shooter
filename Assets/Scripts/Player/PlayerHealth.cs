using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private int health = 5;

    [SerializeField] private Image[] shieldBars;

    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private Transform weaponCamera;

    [SerializeField] private int deathCameraPriority = 20;

    [SerializeField] private GameObject gameOverContainer;

    private void Awake()
    {
        AdjustShieldUI();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        AdjustShieldUI();

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    private void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < health)
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }

    private void HandleDeath()
    {
        weaponCamera.SetParent(null);
        virtualCamera.Priority = deathCameraPriority;

        if (gameOverContainer != null)
        {
            gameOverContainer.SetActive(true);
        }

        Destroy(gameObject);
    }
}