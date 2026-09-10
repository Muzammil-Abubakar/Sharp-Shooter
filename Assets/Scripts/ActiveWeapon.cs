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
    private bool wasZooming;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        animator = GetComponent<Animator>();
        input = GetComponentInParent<StarterAssetsInputs>();
        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        HandleShooting();
        HandleZoom();

        wasShooting = input.shoot;
        wasZooming = input.zoom;
    }

    private void HandleShooting()
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
    }

    private void HandleZoom()
    {
        bool zoomStarted = input.zoom && !wasZooming;
        bool zoomReleased = !input.zoom && wasZooming;

        // Zoom started
        if (zoomStarted)
        {
            if (weaponSO.canZoom)
            {
                Debug.Log("Zoom Start");
            }
            else
            {
                Debug.Log("Cannot Zoom: Current weapon cannot zoom.");
            }
        }

        // Zoom held
        if (input.zoom && weaponSO.canZoom)
        {
            Debug.Log("Zoom Held");
        }

        // Zoom released
        if (zoomReleased && weaponSO.canZoom)
        {
            Debug.Log("Zoom Release");
        }
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