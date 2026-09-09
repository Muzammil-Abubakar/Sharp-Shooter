using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private float rotationSpeed = 100f;

    private ActiveWeapon activeWeapon;

    void Awake()
    {
        activeWeapon = FindAnyObjectByType<ActiveWeapon>();
    }

    void Update()
    {
        rotateTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activeWeapon.SwitchWeapon(weaponSO);
            Destroy(gameObject);
        }
    }
}