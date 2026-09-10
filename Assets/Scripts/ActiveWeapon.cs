using UnityEngine;
using StarterAssets;
using Unity.Cinemachine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    [Header("Zoom")]
    [SerializeField] private GameObject zoomVignette;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private Animator animator;
    private StarterAssetsInputs input;
    private Weapon weapon;

    // Found automatically from the parent.
    private FirstPersonController firstPersonController;

    private float nextFireTime;
    private bool wasShooting;
    private bool wasZooming;

    private const float DEFAULT_FOV = 40f;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        animator = GetComponent<Animator>();
        input = GetComponentInParent<StarterAssetsInputs>();
        weapon = GetComponentInChildren<Weapon>();

        // Find the FirstPersonController from the parent.
        firstPersonController = GetComponentInParent<FirstPersonController>();

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

    public void SwitchWeapon(WeaponSO newWeaponSO)
    {
        // Destroy the current weapon
        Weapon currentWeapon = GetComponentInChildren<Weapon>();

        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        // Reset zoom when switching weapons.
        SetZoom(false);
        SetCameraFOV(DEFAULT_FOV);
        ChangeRotationSpeed(DEFAULT_FOV);

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