using UnityEngine;
using StarterAssets;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    private Animator animator;
    private Camera mainCamera;
    private StarterAssetsInputs input;
    private Weapon weapon;
    private float nextFireTime;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        input = GetComponentInParent<StarterAssetsInputs>();
        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        if (input.shoot && Time.time >= nextFireTime)
        {
            weapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            nextFireTime = Time.time + weaponSO.FireRate;
        }
    }
}