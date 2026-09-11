using UnityEngine;

public class AmmoPickup : Pickup
{
    [Header("Ammo Pickup")]
    [SerializeField] private int ammoAmount = 30;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 90f;

    private void Update()
    {
        // Rotate around the Y axis.
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    protected override void OnPickup(Collider other)
    {
        ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();

        if (activeWeapon == null)
        {
            activeWeapon = other.GetComponentInParent<ActiveWeapon>();
        }

        if (activeWeapon == null)
        {
            Debug.LogWarning("AmmoPickup could not find an ActiveWeapon on the player.");
            return;
        }

        // Add ammo. ActiveWeapon will clamp it to the weapon's MagazineSize.
        activeWeapon.IncreaseAmmo(ammoAmount);

        // Remove the pickup after collecting it.
        Destroy(gameObject);
    }
}