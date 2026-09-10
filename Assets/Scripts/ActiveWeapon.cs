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

    public void SwitchWeapon(WeaponSO newWeaponSO)
    {
        // Destroy the current weapon
        Weapon currentWeapon = GetComponentInChildren<Weapon>();

        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        // Update the weapon data
        weaponSO = newWeaponSO;

        // Spawn the new weapon
        GameObject newWeapon = Instantiate(
            weaponSO.WeaponPrefab,
            transform
        );

        // Set the weapon to the local origin
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;

        // Get the Weapon component from the new weapon
        weapon = newWeapon.GetComponent<Weapon>();
    }
}