using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private float rotationSpeed = 100f;

    private ActiveWeapon activeWeapon;

    private void Awake()
    {
        activeWeapon = FindAnyObjectByType<ActiveWeapon>();
    }

    private void Update()
    {
        rotateTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    protected override void OnPickup(Collider other)
    {
        if (activeWeapon == null)
        {
            Debug.LogWarning("WeaponPickup could not find an ActiveWeapon.");
            return;
        }

        activeWeapon.SwitchWeapon(weaponSO);
        Destroy(gameObject);
    }
}