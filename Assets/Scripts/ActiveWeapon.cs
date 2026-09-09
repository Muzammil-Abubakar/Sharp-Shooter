using UnityEngine;
using StarterAssets;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    private Animator animator;
    private StarterAssetsInputs input;
    private Weapon weapon;
    private float nextFireTime;
    private bool wasShooting;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        animator = GetComponent<Animator>();
        input = GetComponentInParent<StarterAssetsInputs>();
        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        bool shootPressed = input.shoot && !wasShooting;

        if (weaponSO.isAutomatic ? input.shoot : shootPressed)
        {
            if (Time.time >= nextFireTime)
            {
                weapon.Shoot(weaponSO);
                animator.Play(SHOOT_STRING, 0, 0f);
                nextFireTime = Time.time + weaponSO.FireRate;
            }
        }

        wasShooting = input.shoot;
    }
}