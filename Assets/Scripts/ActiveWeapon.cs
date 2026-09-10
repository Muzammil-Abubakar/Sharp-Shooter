using UnityEngine;
using TMPro;
using StarterAssets;
using Unity.Cinemachine;

public class ActiveWeapon : MonoBehaviour
{
    [Header("Starting Weapon")]
    [SerializeField] private WeaponSO startingWeaponSO;

    [Header("Ammo")]
    [SerializeField] private TMP_Text ammoText;

    [Header("Zoom")]
    [SerializeField] private GameObject zoomVignette;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private Animator animator;
    private StarterAssetsInputs input;
    private Weapon weapon;

    // Found automatically from the parent.
    private FirstPersonController firstPersonController;

    private WeaponSO weaponSO;

    private int currentAmmo;

    private float nextFireTime;
    private bool wasShooting;
    private bool wasZooming;

    private const float DEFAULT_FOV = 40f;
    private const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        animator = GetComponent<Animator>();
        input = GetComponentInParent<StarterAssetsInputs>();

        // Find the FirstPersonController from the parent.
        firstPersonController = GetComponentInParent<FirstPersonController>();

        // Set the current weapon data to the starting weapon.
        weaponSO = startingWeaponSO;

        // Spawn the starting weapon.
        SpawnWeapon();

        // Initialize ammo from the starting weapon.
        if (weaponSO != null)
        {
            currentAmmo = weaponSO.MagazineSize;
            UpdateAmmoText();
        }

        // Make sure zoom starts disabled.
        SetZoom(false);

        // Make sure the camera starts at the default FOV.
        SetCameraFOV(DEFAULT_FOV);

        // Make sure rotation speed starts at the default 40 FOV value.
        if (firstPersonController != null)
        {
            firstPersonController.ChangeRotationSpeed(DEFAULT_FOV);
        }
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
                // Don't shoot if we have no ammo.
                if (currentAmmo <= 0)
                {
                    return;
                }

                // Decrease ammo when shooting.
                DecreaseAmmo();

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
                SetZoom(true);
                SetCameraFOV(weaponSO.ZoomAmount);
                ChangeRotationSpeed(weaponSO.ZoomAmount);
            }
            else
            {
                Debug.Log("Cannot Zoom: Current weapon cannot zoom.");
            }
        }

        // Zoom held
        if (input.zoom && weaponSO.canZoom)
        {
            SetZoom(true);
            SetCameraFOV(weaponSO.ZoomAmount);
            ChangeRotationSpeed(weaponSO.ZoomAmount);
        }

        // Zoom released
        if (zoomReleased)
        {
            SetZoom(false);
            SetCameraFOV(DEFAULT_FOV);
            ChangeRotationSpeed(DEFAULT_FOV);
        }
    }

    private void SpawnWeapon()
    {
        if (weaponSO == null)
        {
            Debug.LogWarning("ActiveWeapon does not have a Starting Weapon SO assigned.");
            return;
        }

        if (weaponSO.WeaponPrefab == null)
        {
            Debug.LogWarning("The Starting Weapon SO does not have a Weapon Prefab assigned.");
            return;
        }

        GameObject newWeapon = Instantiate(
            weaponSO.WeaponPrefab,
            transform
        );

        // Set the weapon to the local origin.
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;

        // Get the Weapon component from the new weapon.
        weapon = newWeapon.GetComponent<Weapon>();

        if (weapon == null)
        {
            Debug.LogWarning(
                $"The weapon prefab '{newWeapon.name}' does not contain a Weapon component."
            );
        }
    }

    private void SetZoom(bool isZooming)
    {
        if (zoomVignette != null)
        {
            zoomVignette.SetActive(isZooming);
        }
    }

    private void SetCameraFOV(float fov)
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Lens.FieldOfView = fov;
        }
    }

    private void ChangeRotationSpeed(float fov)
    {
        if (firstPersonController != null)
        {
            firstPersonController.ChangeRotationSpeed(fov);
        }
    }

    public void IncreaseAmmo(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        currentAmmo += amount;
        UpdateAmmoText();
    }

    public void DecreaseAmmo(int amount = 1)
    {
        if (amount <= 0)
        {
            return;
        }

        currentAmmo -= amount;
        currentAmmo = Mathf.Max(currentAmmo, 0);

        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo.ToString();
        }
    }

    public void SwitchWeapon(WeaponSO newWeaponSO)
    {
        if (newWeaponSO == null)
        {
            Debug.LogWarning("Cannot switch weapon because the new WeaponSO is null.");
            return;
        }

        // Destroy the current weapon.
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }

        // Reset zoom when switching weapons.
        SetZoom(false);
        SetCameraFOV(DEFAULT_FOV);
        ChangeRotationSpeed(DEFAULT_FOV);

        // Update the current weapon data.
        weaponSO = newWeaponSO;

        // Reset ammo using the new weapon's magazine size.
        currentAmmo = weaponSO.MagazineSize;
        UpdateAmmoText();

        // Spawn the new weapon.
        SpawnWeapon();
    }
}